using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Grpc.Core;
using MagicOnion.Client;
using MagicOnionDotNet6.Shared;
using UniRx;
using UnityEngine;
using Channel = Grpc.Core.Channel;
using Random = UnityEngine.Random;

namespace MagicOnion_DotNet6.PongOnion.Scripts
{
    public class GameManager : MonoBehaviour, IGamingHubReceiver
    {
        public readonly CancellationTokenSource shutdownCancellation = new();
        private const string TEST_ROOM_NAME = "TEST_ROOM";
        private readonly TimeSpan SendInterval = TimeSpan.FromMilliseconds(50);
        private readonly UniTaskCompletionSource _waitForMatching = new();

        private Channel _channel;
        private IGamingHub _gamingHub;
        private string _id;
        private Player _self;
        private Player _other;

        private bool _isGameStarted;
        private bool _canHandleBall;

        [SerializeField] private PaddleController _leftPaddle;
        [SerializeField] private PaddleController _rightPaddle;
        [SerializeField] private BallController _ballController;
        [SerializeField] private CanvasController _canvasController;
        [SerializeField] private InputEventProvider _inputEventProvider;

        private PaddleController LocalPaddle => _self.IsLeft ? _leftPaddle : _rightPaddle;
        private PaddleController RemotePaddle => !_self.IsLeft ? _leftPaddle : _rightPaddle;

        private async void OnDestroy()
        {
            shutdownCancellation.Cancel();
            if (_gamingHub != null) await _gamingHub.DisposeAsync();
            if (_channel != null) await _channel.ShutdownAsync();
        }

        private async void Start()
        {
            InitCanvas();
            await InitConnection();
            InitGame();
            StopGame();
        }

        private void InitCanvas()
        {
            _canvasController.SetButtonActive(true);
            _canvasController.SetButtonInteractable(false);
            _canvasController.SetButtonText("Connecting");
            _canvasController.OnRestartButtonClickedAsObservable.Subscribe(_ =>
            {
                _gamingHub.RestartGame();
                _canvasController.SetButtonInteractable(false);
                _canvasController.SetButtonText("Waiting");
            }).AddTo(this);
        }

        private async Task InitConnection()
        {
            // サーバに接続
            var host = "localhost";
            var port = 5001;
            _channel = new Channel(host, port, ChannelCredentials.Insecure);

            while (!shutdownCancellation.IsCancellationRequested)
            {
                try
                {
                    Debug.Log("Connecting");
                    _gamingHub = await StreamingHubClient.ConnectAsync<IGamingHub, IGamingHubReceiver>(_channel, this,
                        cancellationToken: shutdownCancellation.Token);
                    break;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }

                Debug.Log("Failed to connect");
                await Task.Delay(5000);
            }

            // ルームに参加してルームの参加者一覧を受け取る
            var players = (await _gamingHub.JoinAsync(TEST_ROOM_NAME, _id = Guid.NewGuid().ToString())).ToList();
            _self = players.First(p => p.Id == _id);
            _other = players.FirstOrDefault(p => p.Id != _id);

            _canvasController.SetButtonText("Connected");

            // 他の参加者を待つ
            if (players.Count == 1)
            {
                _canvasController.SetButtonText("Matching");
                await _waitForMatching.Task;
            }
        }

        private void InitGame()
        {
            _inputEventProvider.IsAuto = !_self.IsLeft;

            // パドル操作
            _inputEventProvider.Up.Subscribe(keyDown =>
            {
                if (!_isGameStarted || !keyDown) return;
                var pos = LocalPaddle.Position + LocalPaddle.DefaultDeltaPosition;
                LocalPaddle.Move(pos);
            }).AddTo(this);
            _inputEventProvider.Down.Where(_ => _isGameStarted).Subscribe(keyDown =>
            {
                if (!_isGameStarted || !keyDown) return;
                var pos = LocalPaddle.Position - LocalPaddle.DefaultDeltaPosition;
                LocalPaddle.Move(pos);
            }).AddTo(this);

            // 位置をサーバーに送信
            Observable.Interval(SendInterval).Subscribe(_ =>
            {
                if (_isGameStarted) _gamingHub.MovePlayerAsync(LocalPaddle.Position);
                if (_canHandleBall) _gamingHub.MoveBallAsync(_ballController.Position, _ballController.Velocity);
            }).AddTo(this);

            // 衝突判定
            _ballController.OnCollision.Subscribe(collision =>
            {
                Debug.Log(collision);
                var localPaddleCollision = _self.IsLeft ? CollisionObjects.Paddle_L : CollisionObjects.Paddle_R;

                // 自Paddleにぶつかるとボールの所有権を手に入れる
                if (collision == localPaddleCollision)
                {
                    // 弾性衝突 + 摩擦
                    var ballVelocity = _ballController.Velocity;
                    ballVelocity.x = -ballVelocity.x;
                    ballVelocity.y += LocalPaddle.Velocity.y * LocalPaddle.Fliction;
                    _ballController.Velocity = ballVelocity;
                    _canHandleBall = true;
                    _gamingHub.MoveBallAsync(_ballController.Position, ballVelocity);
                    _gamingHub.TakeBallOwnership(_id);
                    return;
                }

                if (!_canHandleBall) return;

                switch (collision)
                {
                    // 弾性衝突
                    case CollisionObjects.Wall:
                        var ballVelocity = _ballController.Velocity;
                        ballVelocity.y = -ballVelocity.y;
                        _ballController.Velocity = ballVelocity;
                        break;

                    // 自分の勝利
                    case CollisionObjects.Goal_R:
                        _gamingHub.GoalAsync(_self.IsLeft ? _self.Id : _other.Id);
                        break;

                    // 相手の勝利
                    case CollisionObjects.Goal_L:
                        _gamingHub.GoalAsync(_self.IsLeft ? _other.Id : _self.Id);
                        break;
                }
            }).AddTo(this);
        }

        private void RestartGame()
        {
            _canvasController.SetButtonActive(false);
            _isGameStarted = true;

            _leftPaddle.Move(0);
            _rightPaddle.Move(0);

            if (_canHandleBall)
            {
                var x = Random.value > 0.5f ? Vector2.up : Vector2.down;
                var y = Random.value > 0.5f ? Vector2.left : Vector2.right;
                _ballController.AddInitialVelocity(x + y);
            }
        }

        private void StopGame()
        {
            _isGameStarted = false;
            _leftPaddle.Move(0);
            _rightPaddle.Move(0);
            _ballController.ResetPosition();
            _canvasController.SetButtonActive(true);
            _canvasController.SetButtonInteractable(true);
            _canvasController.SetButtonText(null);
        }

        #region IGamingHubReceiver

        public void OnJoin(Player player)
        {
            Debug.Log($"{player.Id}, {player.Point}, {player.IsLeft}");
            if (_waitForMatching.Task.Status == UniTaskStatus.Pending)
            {
                _waitForMatching.TrySetResult();
            }

            _other = player;
        }

        public void OnRestartGame(string ballOwnerId)
        {
            _canHandleBall = ballOwnerId == _id;
            RestartGame();
        }

        public void OnMovePlayer(float position)
        {
            RemotePaddle.Move(position, true);
        }

        public void OnMoveBall(Vector2 position, Vector2 velocity)
        {
            if (_canHandleBall)
            {
                // OnTakeBallOwnershipが来ない限り無視される
                return;
            }

            // velocity も受信できるようにしたので補間いらなくなったのでは？
            _ballController.Move(position, true);
            _ballController.Velocity = velocity;
        }

        public void OnTakeBallOwnership(string id)
        {
            _canHandleBall = id == _id;
        }

        public void OnGoal(Player winner)
        {
            if (winner.IsLeft)
            {
                _canvasController.SetLeftPoint(winner.Point);
            }
            else
            {
                _canvasController.SetRightPoint(winner.Point);
            }
            StopGame();
        }

        public void OnLeave(string id)
        {
            StopGame();
        }

        #endregion
    }
}