using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MagicOnion_DotNet6.PongLocal.Scripts
{
    public class BallController : MonoBehaviour
    {
        [FormerlySerializedAs("_speed")] public float Speed = 0.6f;
        private Rigidbody _rigidbody;
        private readonly Vector3 InitPosition = Vector3.zero;
        public Action<LeftOrRight> OnGoal { get; set; }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void AddInitialForce()
        {
            var x = Random.value > 0.5f ? Vector2.up : Vector2.down;
            var y = Random.value > 0.5f ? Vector2.left : Vector2.right;
            var initVelocity = (x + y).normalized * Speed;
            _rigidbody.AddForce(new Vector3(initVelocity.x, initVelocity.y, 0), ForceMode.VelocityChange);
        }

        public void ResetPosition()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.MovePosition(InitPosition);
        }

        private void FixedUpdate()
        {
            // ガバ物理で玉が止まるのを防ぐ
            if (_rigidbody.velocity.magnitude < 0.5f)
            {
                _rigidbody.velocity *= 1.1f;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Wall")
            {
                var velocity = _rigidbody.velocity;
                velocity.y = -velocity.y;
                _rigidbody.velocity = velocity;
            }
            else if (other.gameObject.tag == "Goal_L")
            {
                OnGoal?.Invoke(LeftOrRight.Left);
            }
            else if (other.gameObject.tag == "Goal_R")
            {
                OnGoal?.Invoke(LeftOrRight.Right);
            }
        }
    }
}