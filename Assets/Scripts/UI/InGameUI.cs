using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealthBar(int value)
    {
        healthBar.value = value;
    }

    public void UpdateStaminaBar(int value)
    {
        staminaBar.value = value;
    }
}
