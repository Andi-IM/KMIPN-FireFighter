using UnityEngine;

namespace Mechanics
{
    /// <summary>
    /// This class used for patrol enemy, so enemy can move left and right randomly.
    /// </summary>
    public class EnemyPatrol : MonoBehaviour
    {
        [Header("Patrol Points")] 
        [SerializeField] private Transform leftEdge;
        [SerializeField] private Transform rightEdge;

        [Header("Enemy")] 
        [SerializeField] private Transform enemy;

        [Header("Movement Parameters")] 
        [SerializeField] private float speed;
        private Vector3 _initScale;
        private bool _movingLeft;

        [Header("Idle Behaviour")] [SerializeField]
        private float idleDuration;

        private float _idleTimer;

        [Header("Enemy Animator")] [SerializeField]
        private Animator anim;

        private static readonly int Moving = Animator.StringToHash("run");

        private void Awake()
        {
            _initScale = enemy.localScale;
        }

        private void OnDisable()
        {
            anim.SetBool(Moving, false);
        }

        private void Update()
        {
            if (_movingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x)
                    MoveInDirection(-1);
                else
                    DirectionChange();
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x)
                    MoveInDirection(1);
                else
                    DirectionChange();
            }
        }

        private void DirectionChange()
        {
            anim.SetBool(Moving, false);
            _idleTimer += Time.deltaTime;

            if (_idleTimer > idleDuration)
                _movingLeft = !_movingLeft;
        }

        private void MoveInDirection(int direction)
        {
            var position = enemy.position;
            _idleTimer = 0;
            anim.SetBool(Moving, true);

            // Make enemy face direction
            enemy.localScale = new Vector3(Mathf.Abs(_initScale.x) * direction, _initScale.y, _initScale.z);

            // Move in that direction
            enemy.position = new Vector3(position.x + Time.deltaTime * direction * speed,
                position.y, position.z);
        }
    }
}