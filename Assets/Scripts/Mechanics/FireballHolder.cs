using UnityEngine;

namespace Mechanics
{
    public class FireballHolder : MonoBehaviour
    {
        [SerializeField] private Transform enemy;
        private void Update()
        {
            transform.localScale = enemy.localScale;
        }
    }
}
