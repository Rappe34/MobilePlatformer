using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class SettingsSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener((v) => {
            sliderText.text = Mathf.RoundToInt(v * 100f).ToString();
        });
    }
}
