using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


  public class Health : MonoBehaviour
  {
      //[SerializeField] private int maxHealth = 100;

      public float health;

      public event Action takeDamage;

      public event Action Die;

      public GameObject player;

      private void Start()
      {
        player = GameObject.FindWithTag("Player");
        health = GetComponent<BaseStats>().GetStat(Stat.Health);
        Debug.Log("start health: " + health);
      }

      public void DealDamage(int damage)
      {
          if (health == 0) { return; }

          health = Mathf.Max(health - damage, 0);

          takeDamage?.Invoke();

        if(health == 0)
        {
            Die?.Invoke();
            AwardExperience();
        }
          Debug.Log(health);
      }

      public float GetPercentage()
      {
        return health; //100 * ( health / GetComponent<BaseStats>().GetStat(Stat.Health));
      }

      private void AwardExperience()
      {
        Experience experience = player.GetComponent<Experience>();
        if (experience == null) return;

        experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
      }


      /* public void TakeDamage(GameObject instigator, float damage)
      {
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        if(healthPoints == 0)
        {
          Die();
          AwardExperience(instigator);
        }
      } */



  }
