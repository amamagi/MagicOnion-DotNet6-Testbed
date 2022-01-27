using System.Threading.Tasks;
using MagicOnion;
using MessagePack;
using UnityEngine;

namespace MagicOnionDotNet6.Shared
{
    public interface IGamingHub : IStreamingHub<IGamingHub, IGamingHubReceiver>
    {
        Task<Player[]> JoinAsync(string roomName, string id);
        Task RestartGame();
        Task MovePlayerAsync(float position);
        Task MoveBallAsync(Vector2 position, Vector2 velocity);
        Task TakeBallOwnership(string id);
        Task GoalAsync(string winnerId);
        Task LeaveAsync();
    }

    public interface IGamingHubReceiver
    {
        void OnJoin(Player player);
        void OnRestartGame(string ballOwnerId);
        void OnMovePlayer(float position);
        void OnMoveBall(Vector2 position, Vector2 velocity);
        void OnTakeBallOwnership(string id);
        void OnGoal(Player winner);
        void OnLeave(string id);
    }

    [MessagePackObject]
    public class Player
    {
        [Key(0)] public string Id { get; set; }
        [Key(1)] public bool IsLeft { get; set; }
        [Key(2)] public int Point { get; set; }
    }
}