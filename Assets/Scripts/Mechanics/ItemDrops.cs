using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics
{
    public class ItemDrops : MonoBehaviour
    {
        public List<ObjectSpawnRate> objects;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void SpawnItemOnDeath()
        {
            GameObject go = GetItem();
            if (go != null)
            {
                GameManager.GetInstance().AddItem(Instantiate(go, transform.position, Quaternion.identity));
            }
        }
        private GameObject GetItem()
        {
            int limit = 0;

            foreach (ObjectSpawnRate osr in objects)
            {
                limit += osr.rate;
            }

            int random = Random.Range(0, limit);

            foreach (ObjectSpawnRate osr in objects)
            {
                if (random < osr.rate)
                {
                    return osr.prefab;
                }
                else
                {
                    random -= osr.rate;
                }
            }
            return null;
        }
    }
}