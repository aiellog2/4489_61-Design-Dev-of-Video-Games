using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;

namespace RPG.Attributes
{
  public class Health : MonoBehaviour
  {
      [SerializeField] float healthPoints = 100f;

      bool isDead = false;

      private void Start()
      {
          healthPoints = GetComponent<BaseStats>().GetHealth();
      }

      public bool IsDead()
      {
        return isDead;
      }

      public void TakeDamage(GameObject instigator, float damage)
      {
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        if(healthPoints == 0)
        {
          Die();
          AwardExperience(instigator);
        }
      }

      public float GetPercentage()
      {
        return 100 * ( healthPoints / GetComponent<BaseStats>().GetHealth());
      }

      private void Die()
      {
        if (isDead) return;

        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
      }

      private void AwardExperience(GameObject instigator)
      {
        Experience experience = instigator.GetComponent<Experience>();
        if (experience == null) return;

        experience.GainExperience(GetComponent<BaseStats>().GetExperienceReward());
      }



      public object CaptureState()
      {
        return healthPoints;
      }

      public void RestoreState(object state)
      {
          healthPoints = (float) state;

          if(healthPoints <= 0)
          {
            Die();
          }
      }

      void Update()
      {

      }
  }

}
