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

    private AttackStats attack;

    private Coroutine regen;

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
            staminaBarUI.value = stamina;

            if(regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(IncreaseStamina());
        }
    }
    public IEnumerator IncreaseStamina()
    {
        yield return new WaitForSeconds(2);

        while(stamina < maxStamina)
        {
            stamina += maxStamina / 50;
            staminaBarUI.value = stamina;
            yield return new WaitForSeconds(0.1f);
        }
        regen = null;
    }
}