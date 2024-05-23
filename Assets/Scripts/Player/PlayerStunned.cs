using UnityEngine;
using HealthSystem;

namespace Player
{
    public class PlayerStunned : StateMachineBehaviour
    {
        [SerializeField] private Shader defaultShader;
        [SerializeField] private Shader flashShader;

        private PlayerInputHandler input;
        private Health health;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            input = animator.GetComponent<PlayerInputHandler>();
            health = animator.GetComponent<Health>();

            input.SetInputEnabled(false);
            health.SetInvincible(true);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            input.SetInputEnabled(true);
            health.SetInvincible(false);
        }
    }
}
