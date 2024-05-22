using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunned : StateMachineBehaviour
{
    private PlayerInputHandler input;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        input = animator.GetComponent<PlayerInputHandler>();
        input.SetInputEnabled(false);
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        input.SetInputEnabled(true);
    }
}
