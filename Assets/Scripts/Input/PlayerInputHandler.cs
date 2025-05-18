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

        // Store new input values in the _frameInput variable
        _frameInput = new FrameInput
        {
            Move = _moveInput,
            JumpDown = _jumpTriggered,
            JumpHeld = _jumpHeld,
            AttackDown = _attackTriggered
        };

        ResetInputVariables();
    }

    public void EnableInput()
    {
        inputEnabled = true;
    }

    public void DisableInput()
    {
        inputEnabled = false;
        ResetFrameInput();
    }

    private void ResetInputVariables()
    {
        _moveInput = Vector2.zero;
        _jumpTriggered = false;
        _jumpHeld = false;
        _attackTriggered = false;
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

    public FrameInput GetInput() => _frameInput;

    // Functions for getting input with the new input system

    public void OnMove(InputAction.CallbackContext context)
    {
        //_moveInput = context.action.ReadValue<Vector2>();
    }

    public void Move(Vector2 move)
    {
        _moveInput = move;
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
