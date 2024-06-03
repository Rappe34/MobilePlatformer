using UnityEditor;
using UnityEngine;

public class DamageDealerObstacle : MonoBehaviour
{
    [SerializeField] private DamageType damageType;
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealth health = collision.GetComponent<IHealth>();

        if (health == null) return;

        if (damageType == DamageType.InstantDeath)
            health.TakeDamage(health.currentHealth, Vector2.zero);
        else
            health.TakeDamage(damage, Vector2.up);
    }

    public enum DamageType
    {
        TakeDamage,
        InstantDeath
    }
}
