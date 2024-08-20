using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeGroup : MonoBehaviour
{
    [SerializeField] Item slot1, slot2;
    [SerializeField] PlayerManager manager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (slot1.itemCount > 0 && manager.health != manager.maxHp)
            {
                slot1.itemCount--;
                manager.Heal(10);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (slot2.itemCount > 0 && manager.health != manager.maxHp)
            {
                slot2.itemCount--;
                manager.Heal(20);
            }
        }

    }
}
