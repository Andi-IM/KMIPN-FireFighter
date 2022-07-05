 using UnityEngine;

namespace Mechanics
{
    public class Spawner: MonoBehaviour
    {
        [SerializeField] 
        private float timeToSpawn = 5f;
        private float timeSinceSpawn;
        private ObjectPool_Simple objectPool;

        private void Start()
        {
            objectPool = FindObjectOfType<ObjectPool_Simple>();
        }

        private void Update()
        {
            timeSinceSpawn += Time.deltaTime;
            if (timeSinceSpawn >= timeToSpawn)
            {
                GameObject newCritter = objectPool.GetCritter();
                newCritter.transform.position = transform.position;
                timeSinceSpawn = 0f;
            }
        }
    }
}