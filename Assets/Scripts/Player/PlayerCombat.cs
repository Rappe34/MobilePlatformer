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

    private void Awake()
    {
        stamina = GetComponent<PlayerStamina>();
        anim = GetComponent<Animator>();
    }

    private void Attack()
    {
        print("Commence attack");
        anim.SetTrigger("Attack1");

        inCombat = true;
        attacking = true;
        stamina.UseStamina(1);
    }

    private void ComboAttack()
    {
        print("Commence combo");
        anim.SetTrigger("ComboAttack");

        inCombat = true;
        attacking = true;
        stamina.UseStamina(3);
    }

    public void TryAttack()
    {
        Attack();
    }

    public void TryComboAttack()
    {
        ComboAttack();
    }

    public void HitCheck()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(hitCheck.position, hitCheckRadius, enemyLayer);

        foreach (Collider2D col in hitColliders)
        {
            EnemyHealth health = col.GetComponent<EnemyHealth>();
            if (health != null) health.TakeDamage(baseAttackDamage, col.transform.position - transform.position);
        }
    }

    public void AttackEnd()
    {
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitCheck.position, hitCheckRadius);
    }
}
