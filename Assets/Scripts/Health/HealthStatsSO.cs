using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class stores all of the values used in PLAYER LOGIC RELATED TO THE PLAYER HEALTH SYSTEM (HP amount, hit stun, etc.)

[CreateAssetMenu(menuName = "ScriptableObject/ScriptableHealthStats")]
public class HealthStatsSO : ScriptableObject
{
    [Header("HEALTH")]
    public int maxHealth = 8;

    [Header("HIT STUN")]
    public float HitStunTime = 0.5f;
    public float HitFlashTime = 1.2f;
    public Color HitFlashColor = Color.red;
}
