using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoam : StateMachineBehaviour
{
    [SerializeField] private float speed = 2.5f;

    private Enemy enemy;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float moveDirection = enemy.facingRight ? 1f : -1f;

        enemy.MovementX(moveDirection * speed);

        if (enemy.obstacle == ObstacleType.HighWall || enemy.obstacle == ObstacleType.Drop)
            animator.SetTrigger("Wait");

        if (enemy.seesPlayer)
            animator.SetTrigger("Lurk");

        if (enemy.obstacle == ObstacleType.LowWall)
            animator.SetTrigger("Jump");
    }
}
