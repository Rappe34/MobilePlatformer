using UnityEngine;

public class EnemyAttack : StateMachineBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float timeBetweenComboAttacks;

    private Rigidbody2D rb;
    private Enemy enemy;

    private float timeSinceAttack;
    private float timeSinceComboAttack;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy == null) enemy = animator.GetComponent<Enemy>();
        if (rb == null) rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceAttack += Time.deltaTime;
        timeSinceComboAttack += Time.deltaTime;

        if (enemy.player == null) { animator.SetTrigger(enemy.roamTrigger); return; }

        float direction = enemy.player.position.x - rb.position.x;
        float distance = Vector2.Distance(rb.position, enemy.player.position);

        if (enemy.seesPlayer)
        {
            enemy.LookAtPlayer();

            if (distance <= attackRange)
            {
                enemy.MovementX(0f);

                if (timeSinceAttack > timeBetweenAttacks)
                {
                    timeSinceAttack = 0f;
                    animator.SetTrigger(enemy.attackTrigger);

                    if (timeSinceComboAttack > timeBetweenComboAttacks)
                    {
                        timeSinceComboAttack = 0f;
                        animator.SetTrigger(enemy.comboAttackTrigger);
                    }
                }
            }
            else if (enemy.obstacle == ObstacleType.Wall || enemy.obstacle == ObstacleType.BigDrop)
            {
                enemy.MovementX(0f);
            }
            else
            {
                float moveDirection = Mathf.Sign(direction);
                enemy.MovementX(moveDirection);
            }
        }
        else
        {
            animator.SetTrigger(enemy.roamTrigger);
        }
    }
}
