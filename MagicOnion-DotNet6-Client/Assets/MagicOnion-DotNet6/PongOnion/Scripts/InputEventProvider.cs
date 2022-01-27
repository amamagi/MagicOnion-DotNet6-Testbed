using System;
using UniRx;
using UnityEngine;

namespace MagicOnion_DotNet6.PongOnion.Scripts
{
    public class InputEventProvider : MonoBehaviour
    {
        public IObservable<bool> Up => _up;
        public IObservable<bool> Down => _down;
        public bool IsAuto { get; set; }
        private readonly Subject<bool> _up = new();
        private readonly Subject<bool> _down = new();

        private void Start()
        {
            _up.AddTo(this);
            _down.AddTo(this);
        }

        private void FixedUpdate()
        {
            if (IsAuto)
            {
                _up.OnNext(Mathf.Sin(Mathf.PI * (Time.time + 0.25f) / 2f) > 0f);
                _down.OnNext(Mathf.Sin(Mathf.PI * (Time.time + 0.25f) / 2f) < 0f);
            }
            else
            {
                _up.OnNext(Input.GetKey(KeyCode.W));
                _down.OnNext(Input.GetKey(KeyCode.S));
            }
        }
    }
}