using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public bool isAlive { get; }
    public int currentHealth { get; }

    public void TakeDamage(int amount, Vector2 knockback);
    public void AddHealth(int amount);
}
