using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanels;
    [SerializeField] GameObject pause;



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
    }
}
