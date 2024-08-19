using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanels;
    [SerializeField] GameObject pause;



    bool flipping = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //inventoryPanels.SetActive(!inventoryPanels.activeSelf);
            if (flipping)
            {
                inventoryPanels.transform.position = new Vector3(inventoryPanels.transform.position.x, inventoryPanels.transform.position.y - 1000, 0);
            }
            else
            {
                inventoryPanels.transform.position = new Vector3(inventoryPanels.transform.position.x, inventoryPanels.transform.position.y + 1000, 0);
            }

            flipping = !flipping;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pause.SetActive(!pause.activeSelf);
            if (Time.timeScale == 0) Time.timeScale = 1;
            else Time.timeScale = 0;
        }
    }
}
