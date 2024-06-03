using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private StaminaStatsSO stats;

    public int currentStamina { get; private set; }

    private Coroutine staminaRegenCoroutine;

    private void Start()
    {
        currentStamina = stats.MaxStamina;
        InGameUI.Instance.UpdateStaminaBar(currentStamina);
    }

    public void UseStamina(int amount)
    {
        if (staminaRegenCoroutine != null) StopCoroutine(staminaRegenCoroutine);

        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            InGameUI.Instance.UpdateStaminaBar(currentStamina);

            float regenDelay = currentStamina == 0 ? stats.StaminaRegenDelayZero : stats.StaminaRegenDelay;
            staminaRegenCoroutine = StartCoroutine(StaminaRegen(regenDelay));
        }
    }

    private IEnumerator StaminaRegen(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        while (currentStamina < stats.MaxStamina)
        {
            currentStamina++;
            InGameUI.Instance.UpdateStaminaBar(currentStamina);
            yield return new WaitForSeconds(1 / stats.StaminaRegenRate);
        }

        staminaRegenCoroutine = null;
    }
}
