using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderSetting : MonoBehaviour
{
    [SerializeField] private SliderSettingSO settingData;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    private void Awake()
    {
        float savedValue = settingData.GetValue();
        SetText(Mathf.RoundToInt(savedValue * 100f).ToString());

        slider.onValueChanged.AddListener((v) => {
            SetText(Mathf.RoundToInt(v * 100f).ToString());
        });
    }

    private void SetText(string text)
    {
        sliderText.text = text;
    }
}
