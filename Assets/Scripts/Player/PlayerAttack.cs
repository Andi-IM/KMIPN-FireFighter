using System;
using Constants;
using Mechanics;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Ranged Attack")] [SerializeField]
        private float attackCooldown;

        [SerializeField] private Transform firePoint;
        [SerializeField] private PoolObjectType type;
        [SerializeField] private AudioClip waterSound;

        [Header("Collider Parameter")] [SerializeField]
        private float colliderDistance;

        [SerializeField] private BoxCollider2D playerCollider;

        [Header("Enemy Layer")] [SerializeField]
        private LayerMask enemyLayer;

        // [Header("Kick Attack")] [SerializeField]
        // private float damage;

        [SerializeField] private float range;
        [SerializeField] private AudioClip kickSound;

        private Animator anim;
        private PlayerController playerMovement;
        private float cooldownTimer = Mathf.Infinity;
        private Health enemyHealth;
        private static readonly int Attack1 = Animator.StringToHash("attack");
        private ObjectPool_Simple objectPool;
        
        private void Awake()
        {
            anim = GetComponent<Animator>();
            playerMovement = GetComponent<PlayerController>();
        }

        private void Start()
        {
            objectPool = FindObjectOfType<ObjectPool_Simple>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
                Attack();

            // if (Input.GetMouseButton(1) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
            //     KickAttack();

            cooldownTimer += Time.deltaTime;
        }

        private void Attack()
        {
            // SoundManager.instance.PlaySound(waterballsound);
            anim.SetTrigger(Attack1);
            cooldownTimer = 0;

            // waterBalls[FindFireball()].transform.position = firePoint.position;
            // waterBalls[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
            // ObjectPool.GetInstance().RequestObject(type).Activate(firePoint.position, firePoint.rotation);
            
            GameObject newCritter = objectPool.GetCritter();
            newCritter.transform.position = firePoint.position;
            newCritter.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        }

        // private void KickAttack()
        // {
        //     // SoundManager.instance.PlaySound(kickSound);
        //     anim.SetTrigger("Kick");
        //     cooldownTimer = 0;
        // }

        private bool EnemyInSight()
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                playerCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(playerCollider.bounds.size.x * range, playerCollider.bounds.size.y,
                    playerCollider.bounds.size.z),
                0,
                Vector2.left,
                0,
                enemyLayer);

            if (hit.collider != null)
                enemyHealth = hit.transform.GetComponent<Health>();

            return hit.collider != null;
        }

        // private void DamageEnemy()
        // {
        //     // If enemy still in range damage him 
        //     if (EnemyInSight())
        //     {
        //         // Damage Enemy
        //         enemyHealth.TakeDamage(damage);
        //     }
        // }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireCube(
        //         collider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        //         new Vector3(collider.bounds.size.x * range, collider.bounds.size.y, collider.bounds.size.z));
        // }

        // private int FindFireball()
        // {
        //     for (var i = 0; i < waterBalls.Length; i++)
        //     {
        //         if (!waterBalls[i].activeInHierarchy) return i;
        //     }
        //     return 0;
        // }
    }
}