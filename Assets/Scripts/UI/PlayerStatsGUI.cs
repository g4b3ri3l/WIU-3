using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsGUI : MonoBehaviour
{
    [SerializeField] PlayerManager manager;
    [SerializeField] RectTransform hpBar, stamBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, manager.health/manager.maxHp * 500);
        stamBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, manager.stamina / manager.maxStamina * 300);

    }
}
