using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MagicOnion_DotNet6.PongOnion.Scripts
{
    public sealed class CanvasController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _leftPoint;
        [SerializeField] private TMP_Text _rightPoint;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _restartButtonText;
        public IObservable<Unit> OnRestartButtonClickedAsObservable => _restartButton.OnClickAsObservable();

        public void SetButtonActive(bool isActive) => _restartButton.gameObject.SetActive(isActive);
        public void SetLeftPoint(int point) => _leftPoint.text = point.ToString();
        public void SetRightPoint(int point) => _rightPoint.text = point.ToString();

        public void SetButtonText(string text)
        {
            _restartButtonText.text = text ?? "Restart";
        }

        public void SetButtonInteractable(bool interactable) => _restartButton.interactable = interactable;
    }
}