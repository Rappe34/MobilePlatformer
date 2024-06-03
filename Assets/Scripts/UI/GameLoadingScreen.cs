using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] Slider slider;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetPanelActive(bool active)
    {
        loadingPanel.SetActive(active);
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
    }
}
