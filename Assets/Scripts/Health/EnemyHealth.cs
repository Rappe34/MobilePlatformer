using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private HealthStatsSO stats;
    [SerializeField] private float invincibilityTime = .5f;
    [SerializeField] private GameObject bloodSplatterEffect;

    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    public bool isAlive { get; private set; } = true;
    public int currentHealth { get; private set; }

    private bool isInvincible = false;
    private float timeSinceHit = 0f;

    private void Start()
    {
        currentHealth = stats.maxHealth;
    }

    private void Update()
    {
        timeSinceHit += Time.deltaTime;
        isInvincible = timeSinceHit <= invincibilityTime;
    }

    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        SplatterEffect();

        if (amount == 0) currentHealth = 0;
        else currentHealth -= amount;

        if (currentHealth <= 0)
            Die();

        timeSinceHit = 0f;

        OnTakeDamage?.Invoke();
    }

    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, stats.maxHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    private void SplatterEffect()
    {
        Instantiate(bloodSplatterEffect, transform.position + Vector3.up, Quaternion.identity);
    }
}
