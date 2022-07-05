using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    /// <summary>
    /// Represents the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [Header("Health")] [SerializeField] private float startingHealth;
        public float CurrentHealth { get; set; }
        private Animator _anim;
        private bool _dead;

        [Header("iFrames")] [SerializeField] private float iFramesDuration;
        [SerializeField] private int numberOfFlashes;
        private SpriteRenderer _spriteRend;

        [Header("Components")] [SerializeField]
        private Behaviour[] components;

        private bool _invulnerable;

        [Header("Death Action")] [SerializeField]
        private UnityEvent deathEvent;

        private static readonly int Hurt = Animator.StringToHash("hurt");
        private static readonly int IsGround = Animator.StringToHash("isGround");
        private static readonly int Dead = Animator.StringToHash("dead");

        private void Awake()
        {
            CurrentHealth = startingHealth;
            _anim = GetComponent<Animator>();
            _spriteRend = GetComponent<SpriteRenderer>();
        }

        public void TakeDamage(float damage)
        {
            if (_invulnerable) return;
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

            if (CurrentHealth > 0)
            {
                // player hurt
                _anim.SetTrigger(Hurt);
                // iframes
                StartCoroutine(Invunerability());
            }
            else
            {

                if (!_dead)
                {
                    // Deactivate all attached component classes
                    foreach (var behaviour in components)
                        behaviour.enabled = false;

                    // die animation
                    _anim.SetBool(IsGround, true);
                    _anim.SetTrigger(Hurt);
                    _anim.SetBool(Dead, true);

                    _dead = true;

                    if (CompareTag("Player"))
                    {
                        deathEvent.Invoke();
                    }
                }
            }
        }

        public void AddHealth(float value)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, startingHealth);
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TakeDamage(1);
            }
        }*/

        private IEnumerator Invunerability()
        {
            _invulnerable = true;
            Physics2D.IgnoreLayerCollision(10, 11, true);
            // Invulnerability duration
            for (int i = 0; i < numberOfFlashes; i++)
            {
                _spriteRend.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
                // ReSharper disable once Unity.InefficientPropertyAccess
                _spriteRend.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }
            Physics2D.IgnoreLayerCollision(10, 11, true);
            _invulnerable = false;
        }

        private void Reset()
        {
            _anim.SetBool(Dead, false);
        }

        // ReSharper disable once UnusedMember.Local
        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}