using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

  public class HealthDisplays : MonoBehaviour
  {

      Health health;

      // Start is called before the first frame update
      private void Awake()
      {
          health = GameObject.FindWithTag("Player").GetComponent<Health>();   //.GetPercentage();
      }

      // Update is called once per frame
      void Update()
     {
         GetComponent<TMP_Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
     }
 }
