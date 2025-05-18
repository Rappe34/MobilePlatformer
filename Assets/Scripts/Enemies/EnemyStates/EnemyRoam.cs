using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoam : StateMachineBehaviour
{
    private Enemy enemy;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy == null) enemy = animator.GetComponent<Enemy>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.player == null) return;

        float moveDirection = enemy.facingRight ? 1f : -1f;

        enemy.MovementX(moveDirection);

        if (enemy.obstacle == ObstacleType.Wall || enemy.obstacle == ObstacleType.SmallDrop || enemy.obstacle == ObstacleType.BigDrop)
            animator.SetTrigger(enemy.waitTrigger);
        else if (enemy.seesPlayer)
            animator.SetTrigger(enemy.lurkTrigger);
    }
}
