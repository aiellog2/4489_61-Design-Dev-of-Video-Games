using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("k"))
        {
          print("WINNER");
          SceneManager.LoadScene("WinScreen");

        }
        else if(Input.GetKeyDown("l"))
        {
          print("LOSER");
          SceneManager.LoadScene("LossScreen");
        }
    }
}
