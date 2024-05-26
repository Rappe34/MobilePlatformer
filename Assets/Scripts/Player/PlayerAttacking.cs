using UnityEngine;

public class PlayerAttacking : StateMachineBehaviour
{
    private PlayerInputHandler input;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        input = animator.GetComponent<PlayerInputHandler>();
        input.DisableInput();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        input.EnableInput();
    }
}
