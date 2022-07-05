using UnityEngine;

namespace Mechanics
{
    public class CritterReturn : MonoBehaviour
    {
        private ObjectPool_Simple objectPool;

        private void Start()
        {
            objectPool = FindObjectOfType<ObjectPool_Simple>();
        }

        private void OnDisable()
        {
            if (objectPool != null) objectPool.ReturnCritter(gameObject);
        }
    }
}