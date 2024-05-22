using System.Collections;
using System.Collections.Generic;
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
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();
        timeSinceAttack += Time.deltaTime;

        float direction = player.position.x - rb.position.x;
        if (Vector2.Distance(rb.position, player.position) > 1f)
        {
            float moveDirection = Mathf.Sign(direction);
            enemy.MovementX(moveDirection * speed);
        }

        if (enemy.seesPlayer && (enemy.obstacleOnPath == ObstacleType.Drop || enemy.obstacleOnPath == ObstacleType.HighWall))
            animator.SetTrigger("Wait");

        if (enemy.seesPlayer && Vector2.Distance(rb.position, player.position) <= attackRange && timeSinceAttack >= timeBetweenAttacks)
        {
            timeSinceAttack = 0f;
            animator.SetTrigger("Attack");
        }

        if (!enemy.seesPlayer)
            animator.SetTrigger("Roam");

        if (enemy.obstacleOnPath == ObstacleType.LowWall)
            animator.SetTrigger("Jump");
    }
}
