using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingButton : MonoBehaviour
{
    [SerializeField] private Item[] materials;
    [SerializeField] private Item craftedItem;
    [SerializeField] private int[] materialsNeeded;
    [SerializeField] private int craftedAmount = 1;
    [SerializeField] private GameObject disabler;
    [SerializeField] Button buttoncraft;
    private int craftcheck = 0;
    private bool craftcheckbool = false;
    void Update()
    {
        craftcheckbool = true;
        for (int i = 0; i < materials.Length; i++) {
            if (materials[i].itemCount < materialsNeeded[i])
            {
                craftcheck+=1;
            }
        }

        if (craftcheck > 0)
        {
            craftcheckbool=false;      
        }

        disabler.SetActive(!craftcheckbool);
        buttoncraft.interactable = craftcheckbool;

        craftcheck = 0;
    }

    public void CraftItem()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].itemCount -= materialsNeeded[i];
        }
        craftedItem.itemCount += craftedAmount;
    }
}
