using UnityEngine;

namespace Mechanics
{
    public class PlayerTeleport : MonoBehaviour
    {
        private GameObject _currentTeleporter;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_currentTeleporter != null)
                {
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