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

    public bool enhancedState = false;

    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChanged;


    private void Update()
    {
        for (int i = 0; i < equipment.Count; i++)
        {
            if (equipment[i] != null){
                if (equipment[i].name == "Plastic Armour" && player.shield <= 0)
                {
                    equipment[i].RemoveFromEquipment();
                }
                else if (equipment[i].itemCount<=0)
                {
                    equipment[i].RemoveFromEquipment();
                }
            }
        }
    }

    public bool Add(Equipment item)
    {
        if (!item.isDefaultItem && item.itemCount > 0)
        {
            if (equipment.Count >= space)
            {
                Debug.Log("Not enough space!");
                return false;
            }

            equipment.Add(item);
            if (item.name == "Plastic Armour") player.Shield();
            else if (item.name == "Enhanced Bullets") enhancedState = true;
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
        else if (item.name == "Enhanced Bullets") enhancedState = false;
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
