using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float stamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaCost;

    public Slider staminaBarUI;
    
    

    private void Start()
    {
        stamina = maxStamina;
        staminaBarUI.maxValue = maxStamina;
    }
    void Update()
    {
        staminaBarUI.value = stamina;
    }
    public void DecreaseStamina()
    {
        if (stamina >= 0)
        {
            stamina -= staminaCost * Time.deltaTime;
        }
    }
    public void IncreaseStamina()
    {
        if (stamina <= maxStamina)
        {
            stamina += staminaCost * Time.deltaTime;
        }
    }
}