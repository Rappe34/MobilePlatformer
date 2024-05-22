using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : StateMachineBehaviour
{
    private Enemy enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.MovementX(0f);
    }
}
