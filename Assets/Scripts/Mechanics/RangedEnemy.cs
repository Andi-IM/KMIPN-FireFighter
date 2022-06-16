using UnityEngine;

namespace Mechanics
{
    public class RangedEnemy : MonoBehaviour
    {
        [Header("Ray detection")] public GameObject playerDetect;
        [SerializeField] private float detectDistance;

        [Header("Attack Parameters")] [SerializeField]
        private float attackCooldown;

        // [SerializeField] private float range;
        // [SerializeField] private int damage;

        [Header("Ranged Attack")] [SerializeField]
        private Transform firePoint;

        [SerializeField] private GameObject[] fireballs;


        // [Header("Collider Parameters")] [SerializeField]
        // private float colliderDistance;
        // [SerializeField] private BoxCollider2D boxCollider;

        [Header("Player Layer")] [SerializeField]
        private LayerMask playerLayer;

        private float _cooldownTimer = Mathf.Infinity;

        [Header("Fireball Sound")] [SerializeField]
        private AudioClip fireballSound;

        // References
        private Animator anim;
        private EnemyPatrol _enemyPatrol;
        private static readonly int Attack = Animator.StringToHash("rangedAttack");

        private void Awake()
        {
            anim = GetComponent<Animator>();
            _enemyPatrol = GetComponentInParent<EnemyPatrol>();
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;

            // Attack only when player in sight?
            if (PlayerInSight())
            {
                if (_cooldownTimer >= attackCooldown)
                {
                    // Attack
                    _cooldownTimer = 0;
                    anim.SetTrigger(Attack);
                }
            }

            if (_enemyPatrol != null)
                _enemyPatrol.enabled = !PlayerInSight();
        }

        // ReSharper disable once UnusedMember.Local
        private void RangedAttack()
        {
            // SoundManager.instance.PlaySound(fireballSound);
            _cooldownTimer = 0;
            fireballs[FindFireball()].transform.position = firePoint.position; 
            fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
        }

        private int FindFireball()
        {
            for (int i = 0; i < fireballs.Length; i++)
            {
                if (!fireballs[i].activeInHierarchy)
                    return i;
            }
            return 0;
        }

        private bool PlayerInSight()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                playerDetect.transform.position,
                Vector2.left * new Vector2(-transform.localScale.x, 0f),
                detectDistance, playerLayer);

            if (hit.collider != null)
            {
                Debug.DrawRay(playerDetect.transform.position,
                    Vector2.left * hit.distance * new Vector2(-transform.localScale.x, 0f), Color.red);
            }

            return hit.collider != null && hit.collider.CompareTag("Player");
        }
    }
}