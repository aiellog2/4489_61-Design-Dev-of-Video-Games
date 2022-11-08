using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public PlayerMovementState playerMovementState;

    public float stamina;
    float maxStamina;

    public Slider staminaBarUI;
    public float decreaseStamina;

    void Start()
    {
        maxStamina = stamina;
        staminaBarUI.maxValue = maxStamina;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            DecreaseStamina();
        }
        else if (stamina != maxStamina)
        {
            IncreaseStamina();
        }
        staminaBarUI.value = stamina;
    }
    public void DecreaseStamina()
    {
        if (stamina >= 0)
        {
            stamina -= decreaseStamina * Time.deltaTime;
        }
    }
    public void IncreaseStamina()
    {
        if (stamina <= maxStamina)
        {
            stamina += decreaseStamina * Time.deltaTime;
        }
    }
}
