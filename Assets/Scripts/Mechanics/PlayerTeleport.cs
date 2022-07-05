using UnityEngine;

namespace Mechanics
{
    public class PlayerTeleport : MonoBehaviour
    {
        private GameObject _currentTeleporter;
        [SerializeField] private AudioClip clip;
        private AudioSource _source;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_currentTeleporter != null)
                {
                    if (_source.isPlaying)
                    {
                        _source.Stop();
                    }
                    _source.PlayOneShot(clip);
                    transform.position = _currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Teleporter"))
            {
                _currentTeleporter = col.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Teleporter"))
            {
                if (other.gameObject == _currentTeleporter)
                {
                    _currentTeleporter = null;
                }
            }
        }
    }
}