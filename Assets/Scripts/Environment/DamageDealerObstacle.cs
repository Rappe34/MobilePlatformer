using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerObstacle : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private DamageType damageType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();

        if (health == null) return;

        if (damageType == DamageType.InstantDeath)
            health.TakeDamage(health.currentHealth);
        else
            health.TakeDamage(damage);
    }

    public enum DamageType
    {
        InstantDeath
    }

}
