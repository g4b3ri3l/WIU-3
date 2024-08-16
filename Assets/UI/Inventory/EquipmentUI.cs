using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform equipmentParent;
    EquipmentManager equipment;
    ItemSlot[] slots;

    void Start()
    {
        equipment = EquipmentManager.instance;
        equipment.onEquipmentChanged += UpdateUI;

        slots = equipmentParent.GetComponentsInChildren<ItemSlot>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < equipment.equipment.Count)
            {
                slots[i].AddItem(equipment.equipment[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
