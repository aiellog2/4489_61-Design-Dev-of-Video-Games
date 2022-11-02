using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

  public class LevelDisplay : MonoBehaviour
  {

      BaseStats baseStats;

      // Start is called before the first frame update
      private void Awake()
      {
          baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();   //.GetPercentage();
      }

      // Update is called once per frame
      void Update()
     {
         GetComponent<TMP_Text>().text = String.Format("{0:0}", baseStats.GetLevel());
     }
 }
