using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningMessage : MonoBehaviour
{
    public GameObject warningText;
    public bool playerCollided;

    void Update()
    {
        if (playerCollided)
        {
            warningText.SetActive(true);
        }
        else
        {
            warningText.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollided = false;
        }
    }
}

