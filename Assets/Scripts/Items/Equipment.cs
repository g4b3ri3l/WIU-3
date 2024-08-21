using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public int armourMod;
    public int weaponMod;

    bool used = false;

    public override void Use()
    {
        base.Use();

        if (!used && itemCount > 0)
        {
            EquipmentManager.instance.Add(this);
            used = true;
        }
        else
        {
            RemoveFromEquipment();
            used = false;
        }
    }
    public void RemoveFromEquipment()
    {
        EquipmentManager.instance.Remove(this);
    }

}