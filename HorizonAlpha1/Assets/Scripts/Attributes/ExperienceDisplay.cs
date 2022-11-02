using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

  public class ExperienceDisplay : MonoBehaviour
  {

      Experience experience;

      // Start is called before the first frame update
      private void Awake()
      {
          experience = GameObject.FindWithTag("Player").GetComponent<Experience>();   //.GetPercentage();
      }

      // Update is called once per frame
      void Update()
     {
         GetComponent<TMP_Text>().text = String.Format("{0:0}", experience.GetPoints());
     }
 }
