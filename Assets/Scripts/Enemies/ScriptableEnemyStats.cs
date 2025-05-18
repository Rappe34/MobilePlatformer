using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Scriptable Enemy Stats")]
public class ScriptableEnemyStats : ScriptableObject
{
    [Header("MOVEMENT")]
    [Tooltip("The top horizontal movement speed")]
    public float MaxSpeed = 3.8f;

    [Tooltip("The enemy's capacity to gain horizontal speed")]
    public float Acceleration = 120;

    [Tooltip("The pace at which the enemy comes to a stop")]
    public float GroundDeceleration = 60;

    [Tooltip("Deceleration in air only after stopping input mid-air")]
    public float AirDeceleration = 30;

    [Tooltip("A constant downward force applied while grounded. Helps on slopes"), Range(0f, -0.5f)]
    public float GroundingForce = -0.05f;

    [Header("JUMP")]
    [Tooltip("The immediate velocity applied when jumping")]
    public float JumpPower = 36;

    [Tooltip("The maximum vertical movement speed")]
    public float MaxFallSpeed = 40;

    [Tooltip("The enemy's capacity to gain fall speed. a.k.a. In Air Gravity")]
    public float FallAcceleration = 110;

    [Header("COMBAT")]
    [Tooltip("How long the enemy stays stunned for when hit")]
    public float HitStunTime = 1f;
}