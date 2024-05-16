using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int baseAttackDamage = 1;
    [SerializeField] private float attackRate = 1.2f;

    private EnemyStateManager sm;
    private Animator anim;

    private bool attackFlag = false;
    private bool attacking = false;
    private float timeSinceAttack = 0f;

    private void Awake()
    {
        sm = GetComponent<EnemyStateManager>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;
    }

    public override State RunCurrentState()
    {
        if (attackFlag && timeSinceAttack >= 1 / attackRate)
        {
            attackFlag = false;
            attacking = true;

            timeSinceAttack = 0f;

            anim.SetTrigger("Attack");
            anim.SetBool("Waiting", false);
        }

        if (!attacking)
            return sm.previousState;

        return this;
    }

    public void AttackFlag()
    {
        attackFlag = true;
    }

    public void AttackHitCheck()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, .2f, LayerMask.GetMask("Player"));

        foreach (Collider2D player in hitEnemies)
        {
            player.gameObject.GetComponent<PlayerHealth>().TakeDamage(baseAttackDamage);
        }
    }

    public void AttackEnd()
    {
        attacking = false;
    }
}
