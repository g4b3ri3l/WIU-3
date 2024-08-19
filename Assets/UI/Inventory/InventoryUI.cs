using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{ 
    public Transform ItemsParent;
    Inventory inventory;
    ItemSlot[] slots;

    public void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = ItemsParent.GetComponentsInChildren<ItemSlot>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
