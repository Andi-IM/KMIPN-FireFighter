using UnityEngine;

namespace Constants
{
    public enum PoolObjectType
    {
        Fireball, Waterball
    }
    
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType type;
        void Start()
        {
            Deactivate();
        }

        public void Activate(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(true);
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
  
        internal bool IsActive()
        {
            return gameObject.activeInHierarchy;
        }
    }
}