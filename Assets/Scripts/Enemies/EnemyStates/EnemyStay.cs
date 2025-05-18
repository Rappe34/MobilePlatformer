using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStay : StateMachineBehaviour
{
    private Enemy enemy;

    private float timeElapsed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy == null) enemy = animator.GetComponent<Enemy>();
        timeElapsed = 0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.player == null) return;

        enemy.MovementX(0f);

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= Random.Range(0.7f, 1.5f))
        {
            enemy.Flip();
            animator.SetTrigger(enemy.roamTrigger);
        }

        if (enemy.seesPlayer)
        {
            animator.SetTrigger(enemy.lurkTrigger);
        }
    }
}
