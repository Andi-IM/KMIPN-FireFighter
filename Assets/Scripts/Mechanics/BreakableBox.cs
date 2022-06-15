using System.Collections;
using Player;
using UnityEngine;

namespace Mechanics
{
    public class BreakableBox : MonoBehaviour
    {
        private ParticleSystem _particle;
        private SpriteRenderer _sr;

        private BoxCollider2D _collider;
        // private AudioSource _audio;

        private void Awake()
        {
            _particle = GetComponentInChildren<ParticleSystem>();
            _sr = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            // _audio = GetComponent<AudioSource>();
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.gameObject.GetComponent<PlayerController>() && col.contacts[0].normal.y > 0.5f)
            {
                StartCoroutine(Break());
            }
        }
        private IEnumerator Break()
        {
            _particle.Play();

            // _audio = false;
            _sr.enabled = false;
            _collider.enabled = false;

            yield return new WaitForSeconds(_particle.main.startLifetime.constantMax);
            Destroy(gameObject);
        }
    }
}