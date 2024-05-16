using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlaying : MonoBehaviour
{
    ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ps.particleCount == 0)
            Destroy(gameObject);
    }
}
