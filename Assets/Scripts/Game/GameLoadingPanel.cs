using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadingPanel : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
