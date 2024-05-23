using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public bool inputEnabled { get; private set; } = true;

    private FrameInput _frameInput;
    private Vector2 _moveInput;
    private bool _jumpTriggered;
    private bool _jumpHeld;
    private bool _attackTriggered;

    private void Update()
    {
        if (!inputEnabled) return;

        _frameInput = new FrameInput
        {
            Move = _moveInput,
            JumpDown = _jumpTriggered,
            JumpHeld = _jumpHeld,
            AttackDown = _attackTriggered
        };

        _jumpTriggered = false;
        _attackTriggered = false;
    }

    public void SetInputEnabled(bool value)
    {
        inputEnabled = value;
        if (!value) ResetFrameInput();
    }

    private void ResetFrameInput()
    {
        _frameInput = new FrameInput
        {
            Move = Vector2.zero,
            JumpDown = false,
            JumpHeld = false,
            AttackDown = false
        };
    }

    public FrameInput GetInput()
    {
        return _frameInput;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.action.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _jumpTriggered = context.action.triggered;
        _jumpHeld = context.action.phase == InputActionPhase.Started || context.action.phase == InputActionPhase.Performed;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        _attackTriggered = context.action.triggered;
    }
}

public struct FrameInput
{
    public Vector2 Move;
    public bool JumpDown;
    public bool JumpHeld;
    public bool AttackDown;
}
