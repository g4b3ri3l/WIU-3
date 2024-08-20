using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanels;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject saveUI;
    [SerializeField] GameObject deathUI;
    [SerializeField] PlayerManager playerManager;

    bool isInventoryActive = false;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanels.SetActive(isInventoryActive);
            isInventoryActive = !isInventoryActive;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pause.SetActive(!pause.activeSelf);
            if (Time.timeScale == 0) Time.timeScale = 1;
            else Time.timeScale = 0;
        }
        deathUI.SetActive(!playerManager.alive);
    }

    public void CloseSaveUI()
    {
        saveUI.SetActive(false);
    }
}
