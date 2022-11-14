using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Health health;

    float maxHealth;
    public Slider healthBarUI;

    void Start()
    {
        maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
        healthBarUI.maxValue = maxHealth;
    }

    void Update()
    {
        healthBarUI.value = health.health;
    }
}
