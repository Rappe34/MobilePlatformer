using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] [Range(.25f, .75f)] private float attackPointRadius;
    [SerializeField] private int baseAttackDamage = 1;
    [SerializeField] private float attackRate = 1.6f;
    [SerializeField] private float comboChargetime = 3f;

    public bool inCombat { get; private set; } = false;
    public bool attacking { get; private set; } = false;

    private Animator anim;
    private float timeSinceAttack = 0f;
    private float timeSinceComboAttack = 0f;
    private bool comboPossible = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;
        timeSinceComboAttack += Time.deltaTime;

        if (timeSinceAttack < 5f)
            inCombat = true;
        else
            inCombat = false;

        if (timeSinceComboAttack > comboChargetime)
            comboPossible = true;
        else
            comboPossible = false;

        anim.SetFloat("TimeSinceAttack", timeSinceAttack);
        anim.SetBool("ComboPossible", timeSinceComboAttack > comboChargetime);
    }

    private void Attack()
    {
        int r3 = Random.Range(1, 3);
        if (r3 == 1) anim.SetTrigger("Attack1");
        else anim.SetTrigger("Attack2");

        attacking = true;
        timeSinceAttack = 0f;
    }

    public void TryAttack()
    {
        if (timeSinceAttack >= 1f / attackRate)
            Attack();
    }

    public void AttackHitCheck()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, .2f, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(baseAttackDamage);
        }
    }

    public void AttackEnd()
    {
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRadius);
    }
}
