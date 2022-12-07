using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
  {
      //[SerializeField] private int maxHealth = 100;


      [SerializeField] float regenerationPercentage = 70;


      public float maxHealth;
      public float health;

      private bool blocking;

      public event Action takeDamage;

      public event Action Die;

      public GameObject player;
      public bool Dead => health == 0;


      public float weaponBonus = 9;
      private float multiplier;
      private void Start()
      {
        GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        player = GameObject.FindWithTag("Player");
        maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
        health = maxHealth;

        Debug.Log("start health: " + maxHealth);
      }

      public void Update()
      {
        //weaponBonus = GetComponent<Weapon>().GetDamage();
        //Debug.Log("weaponBonus" + weaponBonus);
      }

      public void SetBlocking(bool blocking)
      {
        this.blocking = blocking;
      }

      public void DealDamage(float damage)
      {
          if (health == 0) { return; }

          if(blocking) {return; }

          health = Mathf.Max(health - (damage + weaponBonus), 0);

          takeDamage?.Invoke();

        if (health == 0)
        {
            Die?.Invoke();
            AwardExperience();
        }
          Debug.Log(health);
      }

      public void Heal(float amount)
      {
        if (health == 0) { return; }

        if (blocking) { return; }

        health = Mathf.Max(health + amount);
        health = Mathf.Min(health, maxHealth);
      }

      public float GetHealthPoints()
      {
        return health;
      }

      public float GetMaxHealthPoints()
      {
        return GetComponent<BaseStats>().GetStat(Stat.Health);
      }

      public float GetPercentage()
      {
        return health;
      }

      public float GetMultiplier()
      {
        return GetComponent<BaseStats>().GetStat(Stat.Damage);
      }

      private void AwardExperience()
      {
        Experience experience = player.GetComponent<Experience>();
        if (experience == null) return;

        experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
      }

      private void RegenerateHealth()
      {
        float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
        health = Mathf.Max(health, regenHealthPoints);
      }



  }
