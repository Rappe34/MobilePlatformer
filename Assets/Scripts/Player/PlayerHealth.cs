using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private GameObject bloodSplatterEffect;

    protected override void Die()
    {
        GameManager.Instance.LoseGame();

        Instantiate(bloodSplatterEffect, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}
