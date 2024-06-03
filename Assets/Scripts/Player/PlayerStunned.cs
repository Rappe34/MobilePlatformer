using UnityEngine;

public class PlayerStunned : StateMachineBehaviour
{
    [SerializeField] private Shader defaultShader;
    [SerializeField] private Shader flashShader;

    private PlayerInputHandler input;
    private EnemyHealth health;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        input = animator.GetComponent<PlayerInputHandler>();
        health = animator.GetComponent<EnemyHealth>();

        input.DisableInput();
        health.SetInvincible(true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        input.EnableInput();
        health.SetInvincible(false);
    }
}
