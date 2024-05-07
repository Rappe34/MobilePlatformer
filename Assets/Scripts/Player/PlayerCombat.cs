using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRate = 1.8f;

    private Animator anim;
    private float nextAttackTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (nextAttackTime > 0)
            nextAttackTime -= Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, .2f, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }

        nextAttackTime = 1f / attackRate;
    }

    public void TryAttack()
    {
        if (nextAttackTime <= 0f)
            Attack();
    }
}
