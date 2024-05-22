using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStay : StateMachineBehaviour
{
    [SerializeField] private float waitTime = 1.2f;

    private Enemy enemy;

    private float timeElapsed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        timeElapsed = 0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;

        enemy.MovementX(0f);

        if (enemy.seesPlayer && enemy.obstacleOnPath == ObstacleType.None)
            animator.SetTrigger("Lurk");

        if (!enemy.seesPlayer && timeElapsed >= waitTime)
        {
            animator.SetTrigger("Roam");
            enemy.Flip();
        }
    }
}
