using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField] private float force;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PlayFeedback(Vector2 direction)
    {
        StopAllCoroutines();
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        print("Knockback");
    }
}
