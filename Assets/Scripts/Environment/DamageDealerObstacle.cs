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
            health.TakeDamage(0);
        else
            health.TakeDamage(damage);
    }

    public enum DamageType
    {
        TakeDamage,
        InstantDeath
    }
}
