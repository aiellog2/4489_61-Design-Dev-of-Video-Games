using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseStats : MonoBehaviour
{
      [Range(1, 99)]
      [SerializeField] int startingLevel = 1;
      [SerializeField] CharacterClass characterClass;
      [SerializeField] Progression progression = null;
      [SerializeField] GameObject levelUpParticleEffect = null;

      float modifier; //= 1.1

      public event Action onLevelUp;

      int currentLevel = 0;

      private void Start()
      {
        currentLevel = CalculateLevel();
        Experience experience = GetComponent<Experience>();
        if (experience != null)
        {
          experience.onExperienceGained += UpdateLevel;
        }
      }



      private void UpdateLevel()
      {
        int newLevel = CalculateLevel();
        if (newLevel > currentLevel)
        {
          currentLevel = newLevel;
          LevelUpEffect();
          onLevelUp();
          print("Leveled up!");
        }
      }

      private void LevelUpEffect()
      {
        Instantiate(levelUpParticleEffect, transform);
      }

      public float GetStat(Stat stat)
      {
        return progression.GetStat(stat, characterClass, startingLevel);
      }

      public int GetLevel()
      {
        if (currentLevel < 1)
        {
          currentLevel = CalculateLevel();
        }
        return currentLevel;
      }

      private float GetAdditiveModifier(Stat stat)
      {
        float total = 0;
        foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
        {
          foreach (float modifiers in provider.GetAdditiveModifier(stat))
          {
            total += modifier;
          }
        }
        return total;
      }

      public int CalculateLevel()
      {
        Experience experience = GetComponent<Experience>();
        if (experience == null) return startingLevel;

        float currentXP = GetComponent<Experience>().GetPoints();
        int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
        for (int level = 1; level <= penultimateLevel; level++)
        {
            float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
            if (XPToLevelUp > currentXP)
            {
              return level;
            }
        }

        return penultimateLevel + 1;
      }
}
