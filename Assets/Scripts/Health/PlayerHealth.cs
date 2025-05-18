using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private HealthStatsSO stats;
    [SerializeField] private GameObject bloodSplatterEffect;

    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    public bool isAlive { get; private set; } = true;
    public int currentHealth { get; private set; }

    private SpriteRenderer sr;

    private bool isInvincible = false;
    private float timeSinceHit = 0f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = stats.maxHealth;
    }

    private void Update()
    {
        timeSinceHit += Time.deltaTime;
        isInvincible = timeSinceHit <= stats.HitStunTime;
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
        InGameUI.Instance.UpdateHealthBar(currentHealth);
    }

    private IEnumerator HitFlash()
    {
        float timer = 0f;
        Color startColor = sr.color;
        float halfStunTime = stats.HitStunTime / 2;

        while (timer < stats.HitStunTime)
        {
            if (timer < stats.HitStunTime / 2) sr.color = Color.Lerp(sr.color, stats.HitFlashColor, timer / halfStunTime);
            else sr.color = Color.Lerp(stats.HitFlashColor, startColor, (timer - halfStunTime) / halfStunTime);

            timer += Time.deltaTime;
            yield return null;
        }

        sr.color = startColor;
    }

    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, stats.maxHealth);
        InGameUI.Instance.UpdateHealthBar(currentHealth);
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
