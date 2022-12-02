using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.Timeline.Actions;


public class NPC : MonoBehaviour
{
    public InputReader inputreader;

    public GameObject dialoguePanel;
    public GameObject contButton;
    public Text dialogueText;
    public string[] dialogue;

    private int index;

    public float wordSpeed;
    public bool playerCollided;

    public event Action<NPC> OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
    void Update()
    {
        if(playerCollided && Input.GetKeyDown(KeyCode.E))
        {

            Cursor.visible = true;

            if (dialoguePanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if(dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }
    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text+=letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    public void NextLine()
    {
        contButton.SetActive(false);

        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text="";
            StartCoroutine(Typing());
        }
    }
    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerCollided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollided = false;
            ZeroText();
        }
    }
}
