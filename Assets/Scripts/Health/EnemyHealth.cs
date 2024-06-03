using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private HealthStatsSO stats;
    [SerializeField] private float invincibilityTime = .5f;
    [SerializeField] private GameObject bloodSplatterEffect;

    public UnityEvent<Vector2> OnTakeDamage;
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

    public void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        if (isInvincible) return;

        SplatterEffect();

        currentHealth -= amount;
        timeSinceHit = 0f;

        if (currentHealth <= 0)
            Die();

        OnTakeDamage?.Invoke(knockbackDirection);
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
