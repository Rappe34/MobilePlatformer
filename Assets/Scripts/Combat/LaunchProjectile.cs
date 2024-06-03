using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float speed = 10f;

    public void ShootProjectile(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnLocation.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }
}
