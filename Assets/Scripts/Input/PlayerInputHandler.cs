using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    
    [SerializeField] private InputActionAsset _inputAsset;

    private InputActionMap _inputMap;
    private FrameInput _frameInput;

    private Vector2 _moveInput;
    private bool _jumpTriggered;
    private bool _jumpHeld;
    private bool _attackTriggered;

    private void Awake()
    {
        _inputMap = _inputAsset.FindActionMap("Game");
    }

    private void OnEnable()
    {
        _inputMap.Enable();
        _inputMap["Move"].performed += OnMove;
        _inputMap["Move"].canceled += OnMove;
        _inputMap["Jump"].started += OnJumpStarted;
        _inputMap["Jump"].performed += OnJumpPerformed;
        _inputMap["Jump"].canceled += OnJumpCanceled;
        _inputMap["Attack"].performed += OnAttack;
    }

    private void OnDisable()
    {
        _inputMap.Disable();
        _inputMap["Move"].performed -= OnMove;
        _inputMap["Move"].canceled -= OnMove;
        _inputMap["Jump"].started -= OnJumpStarted;
        _inputMap["Jump"].performed -= OnJumpPerformed;
        _inputMap["Jump"].canceled -= OnJumpCanceled;
        _inputMap["Attack"].performed -= OnAttack;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        _jumpTriggered = true;
        _jumpHeld = true;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _jumpHeld = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        _jumpHeld = false;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        _attackTriggered = true;
    }

    private void Update()
    {
        _frameInput = new FrameInput
        {
            Move = _moveInput,
            JumpDown = _jumpTriggered,
            JumpHeld = _jumpHeld,
            AttackDown = _attackTriggered
        };

        // Reset trigger actions
        _jumpTriggered = false;
        _attackTriggered = false;
    }

    public FrameInput GetInput()
    {
        return _frameInput;
    }
}

public struct FrameInput
{
    public Vector2 Move;
    public bool JumpDown;
    public bool JumpHeld;
    public bool AttackDown;
}
