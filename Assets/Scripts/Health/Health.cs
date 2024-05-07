using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public abstract class Health : MonoBehaviour
    {
        private HealthStatsSO stats;

        private int currentHealth;

        private void Start()
        {
            currentHealth = stats.maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (currentHealth - amount <= 0)
            {
                Die();
                return;
            }

            // IMPLEMENT HURT ANIMATION

            currentHealth = amount;
        }

        public void AddHealth(int amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, stats.maxHealth);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
