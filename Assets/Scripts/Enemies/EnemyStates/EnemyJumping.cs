using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumping : StateMachineBehaviour
{
    private Enemy enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemy.Jump();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.grounded)
            animator.SetTrigger("Roam");
    }
}
