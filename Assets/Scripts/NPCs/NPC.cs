using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject NPCImage;
    [SerializeField] private GameObject NPCName;

    [SerializeField] private GameObject interaction;






    [Header("Level1 1")]
    [SerializeField] string[] L1firstTimeDialogue;
    [SerializeField] string[] L1repeatDialogue;
                              
    [Header("Level2")]        
    [SerializeField] string[] L2firstTimeDialogue;
    [SerializeField] string[] L2repeatDialogue;
                             
    [Header("Level3")]       
    [SerializeField] string[] L3firstTimeDialogue;
    [SerializeField] string[] L3repeatDialogue;

    [SerializeField] bool hasSpokenBefore;

    [SerializeField] AudioSource audioSource;  // Reference to the AudioSource component
    [SerializeField] AudioClip SpeakClip;

    private string[] dialogue;



    private int index;


    [SerializeField] private float wordSpeed;
    [SerializeField] private bool playerIsClose;

    [SerializeField] private Button button;

    private void Start()
    {
        hasSpokenBefore = false;
        button.onClick.AddListener(NextLine);
    }

    void Update()
    {
        

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!hasSpokenBefore)
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

        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            NPCImage.GetComponent<Image>().sprite = this.GetComponent<SpriteRenderer>().sprite;
            NPCName.GetComponent<TMP_Text>().text = this.name;

            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                
            }
        }

        //if (dialogueText.text == dialogue[index])
        //{
        //    button.gameObject.SetActive(true);
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
        //SpeakAudio();
        foreach (char letter in dialogue[index].ToCharArray())
        {
            SpeakAudio();
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        button.gameObject.SetActive(false);
        //SpeakAudio();
        if (playerIsClose){
            if (index < dialogue.Length - 1)
            {
                index++;
                dialogueText.text = "";
                StartCoroutine(Typing());
            }
            else
            {
                if (dialogue == L1firstTimeDialogue || dialogue == L2firstTimeDialogue || dialogue == L3firstTimeDialogue)
                {
                    hasSpokenBefore = true;
                }
                if (interaction != null)
                {
                    interaction.SetActive(true);
                }

                zeroText();
            }
        }
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
    private void SpeakAudio()
    {
        audioSource.PlayOneShot(SpeakClip);
    }
   
}
