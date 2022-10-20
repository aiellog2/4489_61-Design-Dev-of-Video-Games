/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;


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



      public float GetPercentage()
      {
        return healthPoints; // 100 * ( healthPoints / GetComponent<BaseStats>().GetHealth());
      }

      private void Die()
      {
        if (isDead) return;

        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
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
  } */
