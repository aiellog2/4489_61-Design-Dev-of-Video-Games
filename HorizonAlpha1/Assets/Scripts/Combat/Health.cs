using System.Collections;
using System.Collections.Generic;
using UnityEngine;


  public class Health : MonoBehaviour
  {
      [SerializeField] private int maxHealth = 100;

      private int health;



      private void Start()
      {
        health = maxHealth;
      }

      public void DealDamage(int damage)
      {
          if (health == 0) { return; }

          health = Mathf.Max(health - damage, 0);

          Debug.Log(health);

      }

      //public float GetPercentage()
      //{
      //  return health = 100 * ( health / GetComponent<BaseStats>().GetHealth());
      //}


      /* public void TakeDamage(GameObject instigator, float damage)
      {
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        if(healthPoints == 0)
        {
          Die();
          AwardExperience(instigator);
        }
      }

      private void AwardExperience(GameObject instigator)
      {
        Experience experience = instigator.GetComponent<Experience>();
        if (experience == null) return;

        experience.GainExperience(GetComponent<BaseStats>().GetExperienceReward());
      } */

  }
