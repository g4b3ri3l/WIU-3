using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempCraft : MonoBehaviour
{
    [SerializeField] GameObject craft;


    bool flipping = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            craft.SetActive(!craft.activeSelf);
        }
    }
}
