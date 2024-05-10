using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    [SerializeField] private float fadeTime = 1f;

    private Animator anim;
    private bool deathFlag = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override State RunCurrentState()
    {
        if (deathFlag)
        {
            //anim.Play("Death");
            deathFlag = false;
        }

        return this;
    }

    public void DeathFlag()
    {
        deathFlag = true;
    }

    public void StartDeathFade()
    {
        StartCoroutine(DeathFade());
    }

    private IEnumerator DeathFade()
    {
        float t = 0;

        while (t < fadeTime)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
