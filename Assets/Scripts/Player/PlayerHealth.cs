using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;

    private int health;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        if (health <= 0) print("Death"); // IMPLEMENT PLAYER DEATH

        health -= amount;
    }

    public void AddHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }
}
