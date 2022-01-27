using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace MagicOnion_DotNet6.PongOnion.Scripts
{
    public class BallController : MonoBehaviour
    {
        public float InitSpeed { get; set; } = 0.6f;
        private Rigidbody _rigidbody;
        public IObservable<CollisionObjects> OnCollision { get; private set; }


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            OnCollision = this.OnCollisionEnterAsObservable()
                .Select(other => TagToCollisionObject(other.gameObject.tag));
        }

        public void AddInitialVelocity(Vector2 vector)
        {
            var initVelocity = vector.normalized * InitSpeed;
            _rigidbody.AddForce(new Vector3(initVelocity.x, initVelocity.y, 0), ForceMode.VelocityChange);
        }

        public void Move(Vector2 position, bool interpolate = false)
        {
            if (interpolate)
            {
                position = Vector2.Lerp(_rigidbody.position, position, 0.5f);
            }

            _rigidbody.MovePosition(position);
        }

        public void ResetPosition()
        {
            _rigidbody.MovePosition(Vector3.zero);
            _rigidbody.velocity = Vector3.zero;
        }

        public Vector2 Velocity
        {
            get => _rigidbody.velocity;
            set => _rigidbody.velocity = value;
        }

        public Vector2 Position => transform.position;


        private static CollisionObjects TagToCollisionObject(string name)
        {
            switch (name)
            {
                case "Wall":
                    return CollisionObjects.Wall;
                case "Paddle_L":
                    return CollisionObjects.Paddle_L;
                case "Paddle_R":
                    return CollisionObjects.Paddle_R;
                case "Ball":
                    return CollisionObjects.Ball;
                case "Goal_L":
                    return CollisionObjects.Goal_L;
                case "Goal_R":
                    return CollisionObjects.Goal_R;
                default:
                    return CollisionObjects.Unknown;
            }
        }
    }
}