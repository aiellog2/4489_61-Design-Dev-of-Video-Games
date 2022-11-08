using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public PlayerMovementState playerMovementState;

    public float stamina;
    public float maxStamina;

    public Slider staminaBarUI;

    public float decreaseStamina;
    // Start is called before the first frame update
    void Start()
    {
        maxStamina = stamina;
        staminaBarUI.maxValue = maxStamina;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseStamina()
    {
        if(stamina != 0)
            stamina -= decreaseStamina * Time.deltaTime;
    }
    public void IncreaseStamina()
    {
            stamina += decreaseStamina * Time.deltaTime;
    }
}
