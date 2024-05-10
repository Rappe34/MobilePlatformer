using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] [Range(.25f, .75f)] private float attackPointRadius;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int baseAttackDamage = 1;
    [SerializeField] private float attackRate = 1.6f;

    private Animator anim;
    private float timeSinceAttack = 0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;
        anim.SetFloat("TimeSinceAttack", timeSinceAttack);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        timeSinceAttack = 0f;
    }

    public void AttackHitCheck()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, .2f, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(baseAttackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRadius);
    }
}
