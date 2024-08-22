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
        else if (used)
        {
            RemoveFromEquipment();
            used = false;
        }
    }
    public void RemoveFromEquipment()
    {
        used = false;
        EquipmentManager.instance.Remove(this);
    }

}