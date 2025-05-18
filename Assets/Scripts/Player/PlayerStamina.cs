using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private StaminaStatsSO stats;

    public int currentStamina { get; private set; }

    private PlayerController playerController;
    private Coroutine staminaRegenCoroutine;
    private float timeSinceStaminaUsed = 0f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        currentStamina = stats.MaxStamina;
        InGameUI.Instance.UpdateStaminaBar(currentStamina);
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
        timeSinceStaminaUsed += Time.deltaTime;

        // Start regen coroutine if enough time has passed and the coroutine isn't already active
        if (staminaRegenCoroutine == null && currentStamina < stats.MaxStamina && timeSinceStaminaUsed > (currentStamina == 0 ? stats.StaminaRegenDelayZero : stats.StaminaRegenDelay))
        {
            staminaRegenCoroutine = StartCoroutine(StaminaRegen());
        }
    }

    public void UseStamina(int amount)
    {
        // Stop coroutine if stamina used during stamina regen
        if (staminaRegenCoroutine != null)
        {
            StopCoroutine(staminaRegenCoroutine);
            staminaRegenCoroutine = null;
        }

        // Remove used stamina and update UI
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, stats.MaxStamina); // Clamp to avoid weird values
        InGameUI.Instance.UpdateStaminaBar(currentStamina);

        // Reset regen start timer
        timeSinceStaminaUsed = 0f;
    }

    private IEnumerator StaminaRegen()
    {
        while (currentStamina < stats.MaxStamina)
        {
            currentStamina++;
            InGameUI.Instance.UpdateStaminaBar(currentStamina);

            float regenRate = stats.StaminaRegenRate;
            if (Mathf.Abs(playerController.PlayerVelocity.x) > 0.1f || !playerController.PlayerGrounded) regenRate = stats.StaminaMovingRegenRate;

            yield return new WaitForSeconds(1 / regenRate);
        }

        staminaRegenCoroutine = null;
    }
}
