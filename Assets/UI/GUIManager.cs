using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
   [SerializeField] GameObject inventoryPanels;
   [SerializeField] GameObject pause;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanels.SetActive(!inventoryPanels.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pause.SetActive(!pause.activeSelf);
            if (Time.timeScale == 0) Time.timeScale = 1;
            else Time.timeScale = 0;
        }
    }
}
