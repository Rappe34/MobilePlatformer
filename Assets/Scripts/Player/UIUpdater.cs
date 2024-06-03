using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIUpdater : MonoBehaviour
{
    public UnityEvent<int> UpdateHealthUI;
    public UnityEvent<int> UpdateStaminaUI;

    public void UpdateHealth(int value)
    {
        UpdateHealthUI?.Invoke(value);
    }

    public void UpdateStamina(int value)
    {
        UpdateStaminaUI?.Invoke(value);
    }
}
