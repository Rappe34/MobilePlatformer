using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
