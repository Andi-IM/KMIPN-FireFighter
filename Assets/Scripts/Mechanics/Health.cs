using UnityEngine;

namespace HealthSystem
{
    /// <summary>
    /// Represents the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity
        /// </summary>
        public int maxHp;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => _currentHp > 0;
        private int _currentHp;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            _currentHp = Mathf.Clamp(_currentHp + 1, 0, maxHp);
        }
        
        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            _currentHp = Mathf.Clamp(_currentHp - 1, 0, maxHp);
        }

        public void Die()
        {
            while (_currentHp > 0) Decrement();
        }

        private void Awake()
        {
            _currentHp = maxHp;
        }
    }
}