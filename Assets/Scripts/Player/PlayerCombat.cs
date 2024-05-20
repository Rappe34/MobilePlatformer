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
    private bool attackPossible = true;
    private bool comboPossible = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;
        timeSinceComboAttack += Time.deltaTime;

        if (timeSinceAttack > 5f) inCombat = false;
        anim.SetBool("InCombat", inCombat);

        attackPossible = timeSinceAttack > 1f / attackRate;
        comboPossible = timeSinceComboAttack > comboChargetime;
    }

    private void Attack(bool combo)
    {
        if (combo) anim.SetTrigger("ComboAttack");
        else if (comboPossible) anim.SetTrigger("Attack1");
        else anim.SetTrigger("Attack2");

        inCombat = true;
        attacking = true;
        timeSinceAttack = 0f;
    }

    public void TryAttack()
    {
        if (attacking && comboPossible) Attack(true);
        else if (timeSinceAttack >= 1f / attackRate) Attack(false);
    }

    public void AttackHitCheck()
    {
        Collider2D[] hitCol = Physics2D.OverlapCircleAll(attackPoint.position, .2f, LayerMask.GetMask("Enemy"));

        foreach (Collider2D col in hitCol)
        {
            col.GetComponent<Health>().TakeDamage(baseAttackDamage, col.transform.position - transform.position);
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
