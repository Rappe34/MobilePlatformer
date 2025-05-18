using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableEnemyStats stats;
    [SerializeField] private HealthStatsSO healthStats;

    [SerializeField] private LayerMask wallCheckLayers;
    [SerializeField] private LayerMask fallCheckLayers;
    [SerializeField] private Transform wallCheckTransform;
    [SerializeField] private Transform fallCheckTransform;
    [SerializeField] private float wallCheckRayLength;
    [SerializeField] private float smallFallCheckRayLength;
    [SerializeField] private float bigFallCheckRayLength;

    [SerializeField] private LayerMask sightLayers;
    [SerializeField] private Transform sightTransform;
    [SerializeField] private float sightRange;
    [SerializeField] private float forceDetectionRange;

    public bool grounded { get; private set; } = true;
    public bool facingRight { get; private set; } = true;
    public bool seesPlayer { get; private set; } = false;
    public ObstacleType obstacle { get; private set; } = ObstacleType.None;
    public bool frozen { get; private set; } = false;

    public Transform player { get; private set; }

    public int waitTrigger { get; private set; } = Animator.StringToHash("Wait");
    public int roamTrigger { get; private set; } = Animator.StringToHash("Roam");
    public int lurkTrigger { get; private set; } = Animator.StringToHash("Lurk");
    public int attackTrigger { get; private set; } = Animator.StringToHash("Attack");
    public int comboAttackTrigger { get; private set; } = Animator.StringToHash("ComboAttack");
    public int takeDamageTrigger { get; private set; } = Animator.StringToHash("TakeDamage");


    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 frameVelocity;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        obstacle = ObstacleCheck();
    }

    private void FixedUpdate()
    {
        frameVelocity.y = rb.velocity.y;
        if (!frozen) ApplyMovement();
    }

    private void OnEnable()
    {
        TimeTickSystem.OnTick_4 += TimeTickSystem_OnTick_4;
    }

    private void OnDisable()
    {
        TimeTickSystem.OnTick_4 -= TimeTickSystem_OnTick_4;
    }

    private void TimeTickSystem_OnTick_4(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        print("Tick!");
        if (player != null) seesPlayer = PlayerInSightCheck();
    }

    private bool PlayerInSightCheck()
    {
        float playerEnemyDistance = Vector2.Distance(transform.position, player.position);

        if (playerEnemyDistance <= 1f) return true;

        if (playerEnemyDistance > sightRange) return false;

        bool facingPlayer = (player.position.x >= transform.position.x && facingRight) || (player.position.x < transform.position.x && !facingRight);
        if (playerEnemyDistance > forceDetectionRange && !facingPlayer) return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, 1.4f, 0f), player.position - transform.position, Mathf.Infinity, sightLayers);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
            return true;

        return false;
    }

    private ObstacleType ObstacleCheck()
    {
        if (Physics2D.Raycast(wallCheckTransform.position, facingRight ? Vector2.right : Vector2.left, wallCheckRayLength, wallCheckLayers))
        {
            return ObstacleType.Wall;
        }
        if (!Physics2D.Raycast(fallCheckTransform.position, Vector2.down, bigFallCheckRayLength, fallCheckLayers))
        {
            return ObstacleType.BigDrop;
        }
        if (!Physics2D.Raycast(fallCheckTransform.position, Vector2.down, smallFallCheckRayLength, fallCheckLayers))
        {
            return ObstacleType.SmallDrop;
        }
        return ObstacleType.None;
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
            frameVelocity.x = Mathf.MoveTowards(rb.velocity.x, targetX * stats.MaxSpeed, stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    public void HitStun()
    {
        frozen = true;
        rb.velocity = Vector2.zero;
        StartCoroutine(HitStun_());
    }

    private IEnumerator HitStun_()
    {
        float timer = 0f;
        float halfStunTime = healthStats.HitStunTime / 2;

        while (timer < healthStats.HitStunTime)
        {
            if (timer < healthStats.HitStunTime / 2) sr.color = Color.Lerp(sr.color, healthStats.HitFlashColor, timer / halfStunTime);
            else sr.color = Color.Lerp(healthStats.HitFlashColor, Color.white, (timer - halfStunTime) / halfStunTime);

            timer += Time.deltaTime;
            yield return null;
        }

        sr.color = Color.white;

        frozen = false;
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
        Gizmos.DrawRay(wallCheckTransform.position, (facingRight ? Vector2.right : Vector2.left) * wallCheckRayLength);
        Gizmos.DrawRay(fallCheckTransform.position, Vector2.down * bigFallCheckRayLength);
        Gizmos.DrawRay(fallCheckTransform.position + new Vector3(-0.05f, -smallFallCheckRayLength, 0f), Vector3.right * 0.1f);

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
    Wall,
    SmallDrop,
    BigDrop
}
