using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private GameObject bloodSplatterEffect;

    protected override void Die()
    {
        Instantiate(bloodSplatterEffect, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}
