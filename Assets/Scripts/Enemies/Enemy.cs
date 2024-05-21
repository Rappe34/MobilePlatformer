using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float wallCheckRayLength, fallCheckRayLength;
    [SerializeField] private float sightHeight = 1.4f;
    [SerializeField] private float forceDetectionRange = 3.5f; // Range at which enemy detects player no matter what
    [SerializeField] private float sightRange = 8f; // Range at which enemy can detect player in front of it
    [SerializeField] private float attackRange = 2f;

    public bool facingRight { get; private set; } = false;
    public bool seesPlayer { get; private set; } = false;
    public bool playerInAttackRange { get; private set; } = false;

    private SpriteRenderer sr;
    private float playerEnemyDistance;

    private void Awake()
    {
        sr = GetComponent<Animator>();
    }

    private void Update()
    {
        // Distance between enemy and player
        playerEnemyDistance = Vector2.Distance(transform.position, player.position);

        // Determine if the enemy sees the player
        seesPlayer = PlayerInSightCheck();
    }

    private bool PlayerInSightCheck()
    {
        if (Physics2D.Raycast(transform.position + new Vector3(0f, sightHeight), player.position - transform.position, forceDetectionRange)) return true;

        if (playerEnemyDistance <= sightRange && (transform.position.x < player.position.x && facingRight) || (transform.position.x > player.position.x && !facingRight))
        {
            if (Physics2D.Raycast(transform.position + new Vector3(0f, sightHeight), player.position - transform.position, sightRange)) return true;
        }

        return false;
    }

    public bool ObstacleOnPathCheck()
    {
        bool wallAhead = Physics2D.Raycast(transform.position + new Vector3(0f, 1f), facingRight ? transform.forward : -transform.forward, wallCheckRayLength, LayerMask.GetMask("Ground", "Platform"));
        bool fallAhead = Physics2D.Raycast(transform.position + new Vector3(facingRight ? 1f : -1f, 1f), -Vector2.up, fallCheckRayLength, LayerMask.GetMask("Ground", "Platform"));

        return wallAhead || fallAhead;
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

    public void Flip()
    {
        facingRight = !facingRight;
        sr.flipX = !facingRight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0f, sightHeight), player.position - transform.position);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, sightHeight), sightRange);
        Gizmos.DrawRay(transform.position + new Vector3(0f, 1f), (facingRight ? -transform.forward : transform.forward) * wallCheckRayLength);
        Gizmos.DrawRay(transform.position + new Vector3(facingRight ? -1f : 1f, 1f), -Vector2.up * fallCheckRayLength);
    }
}
