using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SaveArea : MonoBehaviour
{

    [SerializeField] private TMP_Text savePrompt;
    [SerializeField] private bool inSaveArea;
    [SerializeField] private GameObject saveUI;


    private void Start()
    {
        savePrompt.gameObject.SetActive(false);
        inSaveArea = false;
        saveUI.SetActive(false);
    }

    private void Update()
    {
        if (inSaveArea)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                saveUI.SetActive(true);
              
            }

        }
    }

    public void SaveGame()
    {
        DataPersistanceManager.instance.SaveGame();
        saveUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                savePrompt.gameObject.SetActive(true);
                inSaveArea = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(savePrompt != null)
                {
                    savePrompt.gameObject.SetActive(false);
                    inSaveArea = false;
                }
            }
        }
    }
}
