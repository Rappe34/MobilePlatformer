using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private ScriptablePlayerStats _stats;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private Animator _anim;
    private FrameInput _input;
    private Vector2 _frameVelocity;
    private bool _cachedQueryStartInColliders;

    private PlayerCombat _playerCombat;

    [SerializeField] private GameObject _dustEffect;

    #region Interface

    public Vector2 FrameInput => _input.Move;
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;

    #endregion

    private float _time;
    private bool _facingRight = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _anim = GetComponent<Animator>();

        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;

        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        GetInput();

        if ((_input.Move.x < 0 && _facingRight) || (_input.Move.x > 0 && !_facingRight))
            Flip();
    }

    private void GetInput()
    {
        _input = playerInputHandler.GetInput();

        if (_stats.SnapInput)
        {
            _input.Move.x = Mathf.Abs(_input.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_input.Move.x);
            _input.Move.y = Mathf.Abs(_input.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_input.Move.y);
        }

        if (_input.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        HandleJump();
        HandleDirection();
        HandleGravity();

        ApplyMovement();

        HandleCombat();

        UpdateAnimatorVars();
    }

    #region Collisions

    private float _frameLeftGrounded = float.MinValue;
    public bool _grounded { get; private set; }

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, LayerMask.GetMask("Ground", "Platform"));
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, LayerMask.GetMask("Ground", "Platform"));

        // Hit a Ceiling
        if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
            GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));

            Instantiate(_dustEffect, transform.position, Quaternion.identity);
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
            GroundedChanged?.Invoke(false, 0);
        }

        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }

    #endregion

    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_input.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_playerCombat.attacking) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = _stats.JumpPower;
        Jumped?.Invoke();
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (_playerCombat.attacking) { _frameVelocity.x = 0f; return; }

        if (_input.Move.x == 0)
        {
            var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _input.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = _stats.GroundingForce;
        }
        else
        {
            var inAirGravity = _stats.FallAcceleration;
            if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion

    public void AddKnockBack(Vector2 force)
    {
        _frameVelocity += force;
    }

    private void ApplyMovement() => _rb.velocity = _frameVelocity;

    #region Combat

    private void HandleCombat()
    {
        if (_input.AttackDown && _grounded) _playerCombat.TryAttack();
    }

    #endregion

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
        _facingRight = !_facingRight;

        if (_grounded) Instantiate(_dustEffect, transform.position, Quaternion.identity);
    }

    private void UpdateAnimatorVars()
    {
        _anim.SetBool("Grounded", _grounded);
        _anim.SetFloat("AbsVelocityX", Mathf.Abs(_rb.velocity.x));
        _anim.SetFloat("VelocityY", _rb.velocity.y);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
    }
#endif
}

public interface IPlayerController
{
    public event Action<bool, float> GroundedChanged;

    public event Action Jumped;
    public Vector2 FrameInput { get; }
}
