using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStay : StateMachineBehaviour
{
    [SerializeField] private float waitTime = 1.2f;

    private GameObject player;
    private Enemy enemy;

    private float timeElapsed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = animator.GetComponent<Enemy>();
        timeElapsed = 0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;

        enemy.MovementX(0f);

        if (timeElapsed >= waitTime)
        {
            animator.SetTrigger("Roam");
            enemy.Flip();
        }

        if (player == null) return;

        float direction = Mathf.Sign(player.transform.position.x - enemy.transform.position.x);

        if (enemy.seesPlayer && ((direction > 0 && enemy.facingRight) || (direction < 0 && !enemy.facingRight)))
        {
            animator.SetTrigger("Lurk");
        }
    }
}
