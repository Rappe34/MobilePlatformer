using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private HealthStatsSO stats;
    [SerializeField] private float invincibilityTime = .25f;

    private Animator anim;

    public bool isAlive { get; private set; } = true;
    public int currentHealth { get; private set; }

    private bool isInvincible = false;
    private float timeSinceHit = 0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = stats.maxHealth;
    }

    private void Update()
    {
        timeSinceHit += Time.deltaTime;
        isInvincible = timeSinceHit <= invincibilityTime;
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        if (currentHealth - amount <= 0)
        {
            Die();
            return;
        }

        anim.SetTrigger("TakeDamage");

        currentHealth -= amount;

        timeSinceHit = 0f;
    }

    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, stats.maxHealth);
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} died!");
    }
}
