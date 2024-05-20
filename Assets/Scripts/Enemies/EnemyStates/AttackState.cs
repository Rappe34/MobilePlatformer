using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class AttackState : State
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int baseAttackDamage = 1;
    [SerializeField] private float attackRate = 1f;

    private EnemyStateManager sm;
    private Animator anim;

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
        if (!attacking)
        {
            sm.previousState.StateStartFlag();
            return StateEnd(sm.previousState);
        }

        return this;
    }

    public override void StateStartFlag()
    {
        attacking = true;
        timeSinceAttack = 0f;
        if (Random.Range(0, 2) == 0) anim.SetTrigger("Attack1");
        else anim.SetTrigger("Attack2");
    }

    protected override State StateEnd(State state)
    {
        attacking = false;

        state.StateStartFlag();
        return state;
    }

    public void AttackHitCheck()
    {
        Collider2D[] hitCol = Physics2D.OverlapCircleAll(attackPoint.position, .2f, LayerMask.GetMask("Player"));

        foreach (Collider2D col in hitCol)
        {
            col.gameObject.GetComponent<Health>().TakeDamage(baseAttackDamage, col.transform.position - transform.position);
        }
    }

    public void AttackEnd()
    {
        attacking = false;
    }
}
