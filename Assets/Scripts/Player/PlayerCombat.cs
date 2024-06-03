using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private StaminaStatsSO staminaStats;
    [SerializeField] private Transform hitCheck;
    [SerializeField] [Range(0.5f, 2f)] private float hitCheckRadius;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int baseAttackDamage = 1;
    [SerializeField] private float attackRate = 1.6f;
    [SerializeField] private float comboChargetime = 5f;
    [SerializeField] private float comboClickTimeSpan = 0.8f;

    public bool inCombat { get; private set; } = false;
    public bool attacking { get; private set; } = false;

    private PlayerStamina stamina;
    private Animator anim;
    private bool attackIsCombo = false;
    private bool attackPossible = true;
    private bool comboPossible = true;
    private float timeSinceAttack = 0f;
    private float timeSinceComboAttack = 0f;

    private void Awake()
    {
        stamina = GetComponent<PlayerStamina>();
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

    private void Attack()
    {
        if (comboPossible) anim.SetTrigger("Attack1");
        else anim.SetTrigger("Attack2");

        inCombat = true;
        attacking = true;
        attackIsCombo = false;
        timeSinceAttack = 0f;
        stamina.UseStamina(1);
    }

    private void ComboAttack()
    {
        anim.SetBool("ComboAttack", true);
        inCombat = true;
        attacking = true;
        attackIsCombo = true;
        timeSinceAttack = 0f;
        timeSinceComboAttack = 0f;
        stamina.UseStamina(3);
    }

    public void TryAttack()
    {
        if (timeSinceAttack < comboClickTimeSpan)
        {
            if (comboPossible && stamina.currentStamina < staminaStats.ComboAttackStaminaCost) ComboAttack();
        }

        if (attackPossible && stamina.currentStamina >= staminaStats.AttackStaminaCost) Attack();
    }

    public void HitCheck()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(hitCheck.position, hitCheckRadius, enemyLayer);

        foreach (Collider2D col in hitColliders)
        {
            col.GetComponent<EnemyHealth>().TakeDamage(baseAttackDamage, col.transform.position - transform.position);
        }
    }

    public void AttackEnd()
    {
        if (!attackIsCombo) attacking = false;
    }

    public void ComboAttackEnd()
    {
        attacking = false;
        attackIsCombo = false;
        anim.SetBool("ComboAttack", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitCheck.position, hitCheckRadius);
    }
}
