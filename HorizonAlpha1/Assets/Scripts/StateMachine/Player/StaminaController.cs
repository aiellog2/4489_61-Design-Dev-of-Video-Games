using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{

    public float playerStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    public bool regenerated = true;
    public bool sprinting = false;

    [Range(0,50)][SerializeField] private float staminaDrain = 0.5f;
    [Range(0,50)] [SerializeField] private float staminaRegen = 0.5f;

    [SerializeField] private int slowedRunSpeed = 5;
    [SerializeField] private int normalRunSpeed = 8;

    [SerializeField] private float jumpCost = 20;
  
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;

    private PlayerMovementState playerMovementState;

    private void Update()
    {
        if (!sprinting)
        {
            if (playerStamina <= maxStamina - 0.01)
            {
                playerStamina += staminaRegen * Time.deltaTime;
                UpdateStamina(1);

                if (playerStamina >= maxStamina)
                {
                    
                    sliderCanvasGroup.alpha = 0;
                    regenerated = true;
                }
            }
        }
    }
    public void Sprinting()
    {
        if (regenerated)
        {
            sprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);

            if (playerStamina <= 0)
            {

                regenerated = false;
                sliderCanvasGroup.alpha = 0;
            }
        }
    }
    void UpdateStamina(int value)
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;

        if (value == 0)
        {
            sliderCanvasGroup.alpha = 0;
        }
        else
        {
            sliderCanvasGroup.alpha = 1;
        }
    }
    public void StaminaJump()
    {
        if (playerStamina >= (maxStamina * jumpCost / maxStamina))
        {
            playerStamina -= jumpCost;
            UpdateStamina(1);
        }
    }
}
