using Mechanics;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class HealthBar : MonoBehaviour
    {
        public Health playerHealth;
        public Image totalHealthBar;
        public Image currentHealthBar;

        private void Start()
        {
            totalHealthBar.fillAmount = playerHealth.CurrentHealth * 0.25f;
        }

        private void Update()
        {
            currentHealthBar.fillAmount = playerHealth.CurrentHealth * 0.25f;
        }
    }
}