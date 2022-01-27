using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MagicOnion_DotNet6.PongLocal.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BallController _ball;
        [SerializeField] private PaddleController _leftPaddle;
        [SerializeField] private PaddleController _rightPaddle;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _leftPointText;
        [SerializeField] private TMP_Text _rightPointText;

        private int _leftPoint = 0;
        private int _rightPoint = 0;

        private void Start()
        {
            _ball.OnGoal = Goal;
            _restartButton.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            _ball.AddInitialForce();
            _restartButton.gameObject.SetActive(false);
            _leftPaddle.enabled = true;
            _rightPaddle.enabled = true;
        }

        private void Stop()
        {
            _ball.ResetPosition();
            _restartButton.gameObject.SetActive(true);
            _leftPaddle.enabled = false;
            _rightPaddle.enabled = false;
        }

        private void Goal(LeftOrRight player)
        {
            switch (player)
            {
                case LeftOrRight.Left:
                    _rightPoint++;

                    break;
                case LeftOrRight.Right:
                    _leftPoint++;

                    break;
            }

            _leftPointText.text = _leftPoint.ToString();
            _rightPointText.text = _rightPoint.ToString();

            Stop();
        }
    }
}