using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitCheck : MonoBehaviour
{
    public List<Health> collisionList { get; private set; } = new List<Health>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        collisionList.Add(collision.GetComponent<Health>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;
        Health h = collision.GetComponent<Health>();
        if (collisionList.Contains(h)) collisionList.Remove(h);
    }
}
