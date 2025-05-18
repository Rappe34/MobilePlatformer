using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShutter : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private int shutterCloseTrigger = Animator.StringToHash("ShutterClose");
    private int shutterOpenTrigger = Animator.StringToHash("ShutterOpen");

    public void ShutterClose()
    {
        EnableShutter();
        anim.SetTrigger(shutterCloseTrigger);
    }

    public void ShutterOpen()
    {
        anim.enabled = true;
        anim.SetTrigger(shutterOpenTrigger);
    }

    public void EnableShutter()
    {
        anim.enabled = true;
    }

    public void DisableShutter()
    {
        anim.enabled = false;
    }
}
