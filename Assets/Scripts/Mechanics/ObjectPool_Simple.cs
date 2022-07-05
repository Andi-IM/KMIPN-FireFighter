using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class ObjectPool_Simple : MonoBehaviour
    {
        [SerializeField] private GameObject critterPrefab;
        [SerializeField] private Queue<GameObject> _critterPool = new Queue<GameObject>();
        [SerializeField] private int poolStartSize = 5;
        private void Start()
        {
            for (int i = 0; i < poolStartSize; i++)
            {
                GameObject critter = Instantiate(critterPrefab, transform);
                _critterPool.Enqueue(critter);
                critter.SetActive(false);
            }
        }

        public GameObject GetCritter()
        {
            if (_critterPool.Count > 0)
            {
                GameObject critter = _critterPool.Dequeue();
                critter.SetActive(true);
                return critter;
            }
            else
            {
                GameObject critter = Instantiate(critterPrefab);
                return critter;
            }
        }

        public void ReturnCritter(GameObject critter)
        {
            _critterPool.Enqueue(critter);
            critter.SetActive(false);
        }
    }
}