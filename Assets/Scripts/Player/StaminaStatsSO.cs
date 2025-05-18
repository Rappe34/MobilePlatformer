using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/StaminaStats")]
public class StaminaStatsSO : ScriptableObject
{
    [Header("STAMINA")]
    [Tooltip("Maximum possible stamina")]
    public int MaxStamina = 6;

    [Tooltip("The amount of stamina does jumping use")]
    public int JumpStaminaCost = 1;

    [Tooltip("The amount of stamina attacks use")]
    public int AttackStaminaCost = 1;

    [Tooltip("The amount of stamina combo attacks use")]
    public int ComboAttackStaminaCost = 2;

    [Tooltip("How much stamina is regenerated in a second when the player is standing still on the ground")]
    public float StaminaRegenRate = 1.5f;

    [Tooltip("How much stamina is regenerated in a second when the player is moving")]
    public float StaminaMovingRegenRate = 1.5f;

    [Tooltip("The time before stamina starts to recharge")]
    public float StaminaRegenDelay = 1.5f;

    [Tooltip("The time before stamina starts to recharge when stamina is 0")]
    public float StaminaRegenDelayZero = 2.5f;
}
