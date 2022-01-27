using MagicOnion.Server.Hubs;
using MagicOnionDotNet6.Shared;
using UnityEngine;

namespace MagicOnionDotNet6.Server.Services
{
    public class GamingHub : StreamingHubBase<IGamingHub, IGamingHubReceiver>, IGamingHub
    {
        IGroup room;
        Player self;
        IInMemoryStorage<Player> players;


        public async Task<Player[]> JoinAsync(string roomName, string id)
        {
            //自分の情報を作成し, 保持
            self = new Player
            {
                Id = id,
                Point = 0,
            };

            //ルームに参加し, ルームを保持
            (room, players) = await Group.AddAsync(roomName, self);

            if (players.AllValues.Count > 2) throw new Exception("ごめんなのび太、このゲーム二人用なんだ");

            self.IsLeft = players.AllValues.Count == 1;

            //参加したことを自分以外のルームに参加しているメンバーに通知
            BroadcastExceptSelf(room).OnJoin(self);

            // ルームに入室している他ユーザ全員の情報を配列で取得する
            return players.AllValues.ToArray();
        }

        public async Task LeaveAsync()
        {
            //ルーム内のメンバーから自分を削除
            await room.RemoveAsync(Context);
            //退室したことを全メンバーに通知
            Broadcast(room).OnLeave(self.Id);
        }

        public async Task MoveBallAsync(Vector2 position, Vector2 velocity)
        {
            BroadcastExceptSelf(room).OnMoveBall(position, velocity);
        }

        public async Task MovePlayerAsync(float position)
        {
            BroadcastExceptSelf(room).OnMovePlayer(position);
        }

        public async Task RestartGame()
        {
            Broadcast(room).OnRestartGame(self.Id);
        }
        public async Task GoalAsync(string winnerId)
        {
            var winner = players.AllValues.First(p => p.Id == winnerId);
            winner.Point++;
            Broadcast(room).OnGoal(winner);
        }

        public async Task TakeBallOwnership(string id)
        {
            Broadcast(room).OnTakeBallOwnership(id);
        }
    }
}
