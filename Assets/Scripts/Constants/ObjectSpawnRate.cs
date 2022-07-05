using System;
using UnityEngine;

namespace Constants
{
    [Serializable]
    public class ObjectSpawnRate
    {
        [Range(1, 100)] public int rate;
        public GameObject prefab;
    }
}