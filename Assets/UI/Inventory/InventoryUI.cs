using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform ItemsParent;
    Inventory inventory;
    ItemSlot[] slots;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = ItemsParent.GetComponentsInChildren<ItemSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
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
