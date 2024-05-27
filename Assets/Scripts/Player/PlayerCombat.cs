using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform hitCheck;
    [SerializeField] [Range(0.5f, 2f)] private float hitCheckRadius;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int baseAttackDamage = 1;
    [SerializeField] private float attackRate = 1.6f;
    [SerializeField] private float comboChargetime = 5f;
    [SerializeField] private float comboClickTimeSpan = 0.8f;

    public bool inCombat { get; private set; } = false;
    public bool attacking { get; private set; } = false;

    public UnityEvent OnFinishingAttackLanded;

    private Animator anim;
    private bool attackIsCombo = false;
    private bool attackPossible = true;
    private bool comboPossible = true;
    private float timeSinceAttack = 0f;
    private float timeSinceComboAttack = 0f;

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

    private void Attack()
    {
        if (comboPossible) anim.SetTrigger("Attack1");
        else anim.SetTrigger("Attack2");

        inCombat = true;
        attacking = true;
        attackIsCombo = false;
        timeSinceAttack = 0f;
    }

    private void ComboAttack()
    {
        anim.SetBool("ComboAttack", true);
        inCombat = true;
        attacking = true;
        attackIsCombo = true;
        timeSinceAttack = 0f;
        timeSinceComboAttack = 0f;
    }

    public void TryAttack()
    {
        print(timeSinceAttack);
        if (comboPossible && timeSinceAttack < comboClickTimeSpan) ComboAttack();
        else if (attackPossible) Attack();
    }

    public void HitCheck()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(hitCheck.position, hitCheckRadius, enemyLayer);

        foreach (Collider2D col in hitColliders)
        {
            col.GetComponent<Health>().TakeDamage(baseAttackDamage);
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
