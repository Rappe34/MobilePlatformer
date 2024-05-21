using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnemyAttack : StateMachineBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float attackRange = 3f;

    private Transform player;
    private Rigidbody2D rb;
    private Enemy enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();

        Vector2 target = player.position;
        rb.MovePosition(new Vector2(Mathf.MoveTowards(rb.position.x, target.x, speed * Time.deltaTime), rb.position.y));

        if (!enemy.seesPlayer) animator.SetTrigger("Roam");

        if (enemy.playerInAttackRange) animator.SetTrigger("Attack");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Roam");
        animator.ResetTrigger("Attack");
    }
}
