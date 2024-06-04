using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform hitCheck;
    [SerializeField] [Range(0.5f, 3f)] private float hitCheckRadius;
    [SerializeField] private LayerMask hitLayerMask;
    [SerializeField] private int baseAttackDamage = 1;

    public void AttackHitCheck()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(hitCheck.position, hitCheckRadius, hitLayerMask);

        foreach (Collider2D col in hitColliders)
        {
            col.GetComponent<PlayerHealth>().TakeDamage(baseAttackDamage, col.transform.position - transform.position);
            print("col");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitCheck.position, hitCheckRadius);
    }
}
