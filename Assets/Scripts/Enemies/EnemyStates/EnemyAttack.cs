using UnityEngine;

public class EnemyAttack : StateMachineBehaviour
{
    [SerializeField] private float speed = 3.6f;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;

    private Transform player;
    private Rigidbody2D rb;
    private Enemy enemy;

    private float timeSinceAttack;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();
        timeSinceAttack += Time.deltaTime;

        if (player == null) animator.SetTrigger("Roam");

        float direction = player.position.x - rb.position.x;
        if (Vector2.Distance(rb.position, player.position) > 1f)
        {
            float moveDirection = Mathf.Sign(direction);
            enemy.MovementX(moveDirection * speed);
        }

        if (enemy.seesPlayer && (enemy.obstacle == ObstacleType.Drop || enemy.obstacle == ObstacleType.HighWall))
            animator.SetTrigger("Wait");

        if (enemy.seesPlayer && Vector2.Distance(rb.position, player.position) <= attackRange && timeSinceAttack >= timeBetweenAttacks)
        {
            timeSinceAttack = 0f;
            animator.SetTrigger("Attack");
        }

        if (!enemy.seesPlayer)
            animator.SetTrigger("Roam");

        if (enemy.obstacle == ObstacleType.LowWall)
            animator.SetTrigger("Jump");
    }
}
