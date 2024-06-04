using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableEnemyStats stats;
    [SerializeField] private HealthStatsSO healthStats;

    [Header("GROUND AND OBSTACLE CHECKS")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private BoxCollider2D groundCheckCollider;
    [SerializeField] private BoxCollider2D fallCheckCollider;
    [SerializeField] private float wallCheckRayLength;
    [Header("PLAYER CHECK")]
    [SerializeField] private LayerMask sightLayers;
    [SerializeField] private Transform sightTransform;
    [SerializeField] private float sightRange;
    [SerializeField] private float forceDetectionRange;

    public bool grounded { get; private set; } = true;
    public bool facingRight { get; private set; } = true;
    public bool seesPlayer { get; private set; } = false;
    public ObstacleType obstacle { get; private set; } = ObstacleType.None;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Vector2 frameVelocity;

    private bool freezed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        grounded = GroundCheck();
        obstacle = ObstacleCheck();
        if (player != null) seesPlayer = PlayerInSightCheck();

        if (!freezed)
        {
            HandleGravity();
            ApplyMovement();
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapBox(transform.position, groundCheckCollider.bounds.size, 0f, groundLayers);
    }

    private bool PlayerInSightCheck()
    {
        float playerEnemyDistance = Vector2.Distance(transform.position, player.position);

        if (playerEnemyDistance <= 1f) return true;

        if (playerEnemyDistance > sightRange) return false;

        bool facingPlayer = (player.position.x >= transform.position.x && facingRight) || (player.position.x < transform.position.x && !facingRight);
        if (playerEnemyDistance > forceDetectionRange && !facingPlayer) return false;

        RaycastHit2D hit = Physics2D.Raycast(sightTransform.position, player.position - transform.position, Mathf.Infinity, sightLayers);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
            return true;

        return false;
    }

    private ObstacleType ObstacleCheck()
    {
        if (Physics2D.Raycast(sightTransform.position, Vector3.right, wallCheckRayLength, groundLayers) && grounded) return ObstacleType.HighWall;
        if (!Physics2D.OverlapBox(fallCheckCollider.bounds.center, fallCheckCollider.bounds.size, 0f, groundLayers) && grounded) return ObstacleType.Drop;
        if (Physics2D.Raycast(transform.position + new Vector3(0f, .5f), Vector3.right, wallCheckRayLength, groundLayers) && grounded) return ObstacleType.LowWall;
        else return ObstacleType.None;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && facingRight)
            Flip();

        else if (transform.position.x < player.position.x && !facingRight)
            Flip();
    }

    public void MovementX(float targetX)
    {
        if (targetX == 0)
        {
            var deceleration = grounded ? stats.GroundDeceleration : stats.AirDeceleration;
            frameVelocity.x = Mathf.MoveTowards(rb.velocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            frameVelocity.x = Mathf.MoveTowards(rb.velocity.x, targetX, stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    public void Jump()
    {
        print("jump");
        if (grounded) frameVelocity.y = stats.JumpPower;
    }

    private void HandleGravity()
    {
        if (grounded && frameVelocity.y <= 0f)
        {
            frameVelocity.y = stats.GroundingForce;
        }
        else
        {
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -stats.MaxFallSpeed, stats.FallAcceleration * Time.fixedDeltaTime);
        }
    }

    public void HitStun()
    {
        StartCoroutine(HitStun_());
    }

    private IEnumerator HitStun_()
    {
        freezed = true;
        anim.SetTrigger("TakeDamage");

        float timer = 0f;
        Color startColor = sr.color;

        while (timer < stats.HitStunTime)
        {
            sr.color = Color.Lerp(sr.color, healthStats.HitFlashColor, timer / (stats.HitStunTime / 2));

            yield return null;
        }

        sr.color = startColor;
        freezed = false;
    }

    public void AddKnockback(Vector2 force)
    {
        frameVelocity = force;
    }

    private void ApplyMovement() => rb.velocity = frameVelocity;

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(sightTransform.position, (facingRight ? Vector2.right : Vector2.left) * wallCheckRayLength);
        Gizmos.DrawRay(transform.position + new Vector3(0f, .5f), (facingRight ? Vector2.right: Vector2.left) * wallCheckRayLength);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sightTransform.position, forceDetectionRange);
        if (player != null && Vector2.Distance(sightTransform.position, player.position) <= forceDetectionRange)
            Gizmos.DrawRay(sightTransform.position, player.position - transform.position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sightTransform.position, sightRange);
    }
}

public enum ObstacleType
{
    None,
    LowWall,
    HighWall,
    Drop
}
