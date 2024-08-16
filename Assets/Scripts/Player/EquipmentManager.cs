using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public List<Equipment> equipment = new List<Equipment>();
    public int space = 3;

    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChanged;

    public bool Add(Equipment item)
    {
        if (!item.isDefaultItem)
        {
            if (equipment.Count >= space)
            {
                Debug.Log("Not enough space!");
                return false;
            }

            equipment.Add(item);

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke();
            }
        }

        return true;
    }
    public void Remove(Equipment item)
    {
        equipment.Remove(item);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke();
        }
    }
}
