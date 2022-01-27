using System;
using UnityEngine;

namespace MagicOnion_DotNet6.PongOnion.Scripts
{
    public class PaddleController : MonoBehaviour
    {
        public readonly float Fliction = 0.5f;
        public readonly float Speed = 0.5f; // m/s
        private const float _boundAbs = 0.5f;

        private (float min, float max) _bounds;
        private Rigidbody _rigidbody;

        private void Start()
        {
            var length = transform.localScale.y;
            _bounds = (-_boundAbs + length / 2f, _boundAbs - length / 2f);
            _rigidbody = GetComponent<Rigidbody>();
        }

        public Vector2 Velocity => _rigidbody.velocity;

        public float Position => _rigidbody.position.y;
        public float DefaultDeltaPosition => Time.deltaTime * Speed;

        public void Move(float position, bool interpo = false)
        {
            var pos = transform.position;
            position = Math.Clamp(position, _bounds.min, _bounds.max);
            if (interpo) position = Mathf.Lerp(pos.y, position, 0.5f);
            pos.y = position;

            _rigidbody.MovePosition(pos);
        }
    }
}