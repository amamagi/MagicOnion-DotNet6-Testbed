using System;
using UnityEngine;

namespace MagicOnion_DotNet6.PongLocal.Scripts
{
    public class PaddleController : MonoBehaviour
    {
        private const float _boundAbs = 0.5f;

        public float Speed = 0.5f; // m/s

        [SerializeField] private bool _isLeft;

        private (float min, float max) _bounds;
        private (KeyCode up, KeyCode down) _moveKeySet;
        private Rigidbody _rigidbody;

        private void Start()
        {
            var length = transform.localScale.y;
            _bounds = (-_boundAbs + length / 2f, _boundAbs - length / 2f);
            _moveKeySet = _isLeft ? (KeyCode.W, KeyCode.S) : (KeyCode.UpArrow, KeyCode.DownArrow);
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(_moveKeySet.up))
            {
                Move(Speed * Time.deltaTime);
            }
            else if (Input.GetKey(_moveKeySet.down))
            {
                Move(-Speed * Time.deltaTime);
            }
        }

        private void Move(float deltaY)
        {
            var pos = transform.position;
            pos.y = Math.Clamp(pos.y + deltaY, _bounds.min, _bounds.max);
            _rigidbody.MovePosition(pos);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Ball")
            {
                var velocity = other.rigidbody.velocity;
                velocity.x = -velocity.x;
                velocity.y += _rigidbody.velocity.y * 0.5f;
                other.rigidbody.velocity = velocity;
            }
        }
    
    
    }
}
