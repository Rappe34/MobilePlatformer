using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DamageDealerObstacle : MonoBehaviour
{
    [SerializeField] private DamageType damageType;
    [SerializeField] private int damage = 1;

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
        TakeDamage,
        InstantDeath
    }
}
