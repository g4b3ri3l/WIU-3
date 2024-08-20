using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Level1")]
    [SerializeField] string[] L1firstTimeDialogue;
    [SerializeField] string[] L1repeatDialogue;
                              
    [Header("Level2")]        
    [SerializeField] string[] L2firstTimeDialogue;
    [SerializeField] string[] L2repeatDialogue;
                             
    [Header("Level3")]       
    [SerializeField] string[] L3firstTimeDialogue;
    [SerializeField] string[] L3repeatDialogue;

    [SerializeField] bool hasSpokenBefore;

    public string[] dialogue;



    private int index;


    [SerializeField] private float wordSpeed;
    [SerializeField] private bool playerIsClose;

    [SerializeField] private GameObject button;

    private void Start()
    {
        hasSpokenBefore = false;
    }

    void Update()
    {
        

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(!hasSpokenBefore)
            {
                dialogue = L1firstTimeDialogue;
            }
            else
            {
                dialogue = L1repeatDialogue;
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (!hasSpokenBefore)
            {
                dialogue = L2firstTimeDialogue;
            }
            else
            {
                dialogue = L2repeatDialogue;
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (!hasSpokenBefore)
            {
                dialogue = L3firstTimeDialogue;
            }
            else
            {
                dialogue = L3repeatDialogue;
            }
        }

        int count = 0;

        foreach (string s in dialogue)
        {
            Debug.Log(s);

            count++;
        }

        Debug.Log(dialogue.Length);

        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            //if (dialoguePanel.activeInHierarchy)
            //{
            //    zeroText();
            //}
            //else
            //{
            //    dialoguePanel.SetActive(true);
            //    StartCoroutine(Typing());
            //    button.SetActive(true);
            //}

            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
            button.SetActive(true);
        }


        //if (dialogueText.text == dialogue[index])
        //{
        //    button.SetActive(true);
        //}
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        button.SetActive(false);

        Debug.Log(index);

        //Debug.Log(dialogue[index + 1] != null);

        int count = 0;

        foreach (string s in dialogue)
        {
            count++;

            Debug.Log(s);
        }

        //Debug.Log(count);

        //if (index < dialogue.Length - 1)
        //{
        //    index++;
        //    dialogueText.text = "";
        //    StartCoroutine(Typing());
        //    button.SetActive(true);
        //}
        //else
        //{
        //    zeroText();
        //}

        //if (dialogue[index + 1] != null)
        //{
        //    index++;
        //    dialogueText.text = "";
        //    StartCoroutine(Typing());
        //    button.SetActive(true);
        //}
        //else
        //{
        //    zeroText();
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
