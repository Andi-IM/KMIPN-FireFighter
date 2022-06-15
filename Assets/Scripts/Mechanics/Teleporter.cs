using UnityEngine;

namespace Mechanics
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Transform destination;

        public Transform GetDestination()
        {
            return destination;
        }
    }
}
