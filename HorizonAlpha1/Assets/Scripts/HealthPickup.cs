using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float amount = 30;
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health && health.health < health.maxHealth)
        {
            health.Heal(amount);
            Destroy(gameObject);
        }
    }
}

// UPDATE To use health from basestats
//  health = GetComponent<BaseStats>().GetStat(Stat.Health);
