using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SettingsSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener((v) => {
            sliderText.text = v.ToString();
        });
    }

    public void UpdateSlider()
    {
        sliderText.text = Mathf.RoundToInt(slider.value * 100f).ToString();
    }
}
