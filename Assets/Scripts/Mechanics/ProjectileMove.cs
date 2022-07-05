using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(Moveable))]
    public class ProjectileMove : MonoBehaviour
    {
        private Moveable _moveable;
        // Start is called before the first frame update
        void Start()
        {
            _moveable = GetComponent<Moveable>();
        }

        // Update is called once per frame
        void Update()
        {
            _moveable.SetDirection(transform.right);
        }
    }
}
