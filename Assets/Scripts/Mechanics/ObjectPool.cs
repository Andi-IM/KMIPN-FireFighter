using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Mechanics
{
    public class ObjectPool : MonoBehaviour
    {
        private static ObjectPool _instance;
        public int size;
        public GameObject[] prefabs;
        [SerializeField] private List<PoolObject> poolObjects;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            InstantiateObjects();
        }
    
        private void InstantiateObjects()
        {
            poolObjects = new List<PoolObject>();

            for (int i = 0; i < size; i++)
            {
                foreach (GameObject go in prefabs)
                {
                    poolObjects.Add(Instantiate(go, transform).GetComponent<PoolObject>());
                }   
            }
        }

        public PoolObject RequestObject(PoolObjectType type)
        {
            foreach (PoolObject po in poolObjects)
            {
                // Debug.Log("TYPES " + po.type);
                if (po.type == type && !po.IsActive())
                {
                    return po;
                }
            }
            return null;
        }

        public static ObjectPool GetInstance()
        {
            return _instance;
        }

        public void DeactivateAllObject()
        {
            foreach (PoolObject poolObject in poolObjects)
            {
                poolObject.Deactivate();
            }
        }
    }
}
