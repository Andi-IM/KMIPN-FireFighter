using UnityEngine;

namespace Mechanics
{
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] protected float damage;
        protected void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
                col.GetComponent<Health>().TakeDamage(damage);
        }
    }
}