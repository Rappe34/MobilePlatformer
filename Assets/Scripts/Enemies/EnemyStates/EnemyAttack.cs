using UnityEngine;

public class EnemyAttack : StateMachineBehaviour
{
    [SerializeField] private float speed = 3.6f;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;

    private GameObject player;
    private Rigidbody2D rb;
    private Enemy enemy;

    private float timeSinceAttack;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceAttack += Time.deltaTime;

        if (player == null) animator.SetTrigger("Roam");

        float direction = player.transform.position.x - rb.position.x;
        float distance = Vector2.Distance(rb.position, player.transform.position);

        if (enemy.seesPlayer)
        {
            enemy.LookAtPlayer();

            if (enemy.obstacle == ObstacleType.Drop || enemy.obstacle == ObstacleType.HighWall)
                animator.SetTrigger("Wait");

            if (enemy.obstacle == ObstacleType.LowWall)
                animator.SetTrigger("Jump");

            if (distance <= attackRange)
            {
                enemy.MovementX(0f);

                if (timeSinceAttack >= timeBetweenAttacks)
                {
                    timeSinceAttack = 0f;
                    animator.SetTrigger("Attack");
                }
            }
            else
            {
                float moveDirection = Mathf.Sign(direction);
                enemy.MovementX(moveDirection * speed);
            }
        }
        else
        {
            animator.SetTrigger("Roam");
        }
    }
}
