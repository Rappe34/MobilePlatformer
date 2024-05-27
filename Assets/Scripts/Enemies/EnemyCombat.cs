using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform hitCheck;
    [SerializeField] [Range(0.5f, 1.5f)] private float hitCheckRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int baseAttackDamage = 1;

    public void AttackHitCheck()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(hitCheck.position, hitCheckRadius, playerLayer);

        foreach (Collider2D col in hitColliders)
        {
            col.GetComponent<Health>().TakeDamage(baseAttackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitCheck.position, hitCheckRadius);
    }
}
