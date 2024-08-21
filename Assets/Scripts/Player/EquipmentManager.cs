using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour, IDataPersistance
{
    #region Singleton
    public static EquipmentManager instance;
    private PlayerManager player;

    private void Awake()
    {
        instance = this;
        player = GetComponent<PlayerManager>();
    }

    #endregion

    public List<Equipment> equipment;
    public int space = 2;

    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChanged;

    private void Update()
    {
        if (equipment != null)
        {

        }
    }
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
            if (item.name == "Plastic Armour") player.Shield();
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
        if (item.name == "Plastic Armour") player.ShieldOff();
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke();
        }
    }

    public void LoadData(GameData data)
    {
        this.equipment = data.equipment;
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke();
        }
    }

    public void SaveData(ref GameData data)
    {
        data.equipment = this.equipment;
    }
}
