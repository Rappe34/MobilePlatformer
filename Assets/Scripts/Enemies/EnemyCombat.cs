using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField][Range(0.5f, 1.5f)] private float attackPointRadius;
    [SerializeField] private int baseAttackDamage = 1;

    public void AttackHitCheck()
    {
        Collider2D[] hitCol = Physics2D.OverlapCircleAll(attackPoint.position, .2f, LayerMask.GetMask("Player"));

        foreach (Collider2D col in hitCol)
        {
            col.GetComponent<Health>().TakeDamage(baseAttackDamage, col.transform.position - transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRadius);
    }
}
