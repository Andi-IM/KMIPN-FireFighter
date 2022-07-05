using UnityEngine;

namespace Mechanics
{
    public class Moveable : MonoBehaviour
    {
        public float speed;
        private Vector3 _direction;

        void Update()
        {
            UpdatePosition();
        }
    
        private void UpdatePosition()
        {
            transform.position += NewPosition();
        }

        public Vector3 GetNextPosition()
        {
            return transform.position + NewPosition();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public Vector3 NewPosition()
        {
            return _direction * (Time.deltaTime * speed);
        }

        internal void SetXDirection(float xValue)
        {
            _direction.x = xValue;
        }

        internal void SetYDirection(float yValue)
        {
            _direction.y = yValue;
        }

        public void SetDirection(Vector3 value)
        {
            _direction = value;
        }

        /// <summary>
        ///     Make a Zig Zag movement.
        /// </summary>
        /// <param name="value">Direction</param>
        /// <param name="frequency">Speed of sine movement</param>
        /// <param name="magnitude">Size of sine movement</param>
        public void SetZigZagDirection(Vector3 value, float frequency, float magnitude)
        {
            _direction.x = Mathf.Sin(Time.time * frequency) * magnitude;
            _direction.y = value.y;
        }

        public void SetDirection(Vector2 value)
        {
            _direction.x = value.x;
            _direction.y = value.y;
        }
        internal void SetDirection(float x, float y)
        {
            _direction.x = x;
            _direction.y = y;
        }
    }
}
