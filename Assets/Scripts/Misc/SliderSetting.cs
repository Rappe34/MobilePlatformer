using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetting : MonoBehaviour
{
    [SerializeField] private SliderSettingSO settingData;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    private void Awake()
    {
        float savedValue = settingData.GetValue();
        slider.value = savedValue;
        SetText(Mathf.RoundToInt(savedValue * 100f).ToString());

        slider.onValueChanged.AddListener((v) => {
            print(v);
            SetText(Mathf.RoundToInt(v * 100f).ToString());
            settingData.SetValue(v);
        });
    }

    private void SetText(string text)
    {
        sliderText.text = text;
    }
}
