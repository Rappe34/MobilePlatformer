using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField][Range(0.5f, 1.5f)] private float attackRadius;
    [SerializeField] private int baseAttackDamage = 1;

    public void AttackHitCheck()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(attackPoint.position, attackRadius, Vector2.right, 1f, LayerMask.GetMask("Player"));

        foreach (RaycastHit2D hit in hits)
        {
            hit.collider.GetComponent<Health>().TakeDamage(baseAttackDamage, hit.transform.position - transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
