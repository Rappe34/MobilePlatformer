using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private StaminaStatsSO staminaStats;
    [SerializeField] private Transform hitCheck;
    [SerializeField] [Range(0.5f, 2f)] private float hitCheckRadius;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int baseAttackDamage = 1;

    public bool attacking { get; private set; } = false;
    public bool comboAttacking { get; private set; } = false;

    private PlayerStamina stamina;
    private Animator anim;

    private void Awake()
    {
        stamina = GetComponent<PlayerStamina>();
        anim = GetComponent<Animator>();
    }

    private void TriggerAttack()
    {
        attacking = true;
    }

    public void TryAttack()
    {
        if (stamina.currentStamina >= staminaStats.ComboAttackStaminaCost && attacking && !comboAttacking)
        {
            comboAttacking = true;
            anim.SetTrigger("ComboAttack");
            TriggerAttack();
        }
        else if (stamina.currentStamina >= staminaStats.AttackStaminaCost && !attacking)
        {
            anim.SetTrigger("Attack");
            TriggerAttack();
        }
    }

    public void HitCheck()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(hitCheck.position, hitCheckRadius, enemyLayer);

        foreach (Collider2D col in hitColliders)
        {
            EnemyHealth health = col.GetComponent<EnemyHealth>();
            if (health != null) health.TakeDamage(baseAttackDamage);
        }

        if (comboAttacking) stamina.UseStamina(2);
        else stamina.UseStamina(1);
    }

    public void AttackEnd()
    {
        if (comboAttacking) return;
        attacking = false;
    }

    public void ComboEnd()
    {
        comboAttacking = false;
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitCheck.position, hitCheckRadius);
    }
}
