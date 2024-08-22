using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsGUI : MonoBehaviour
{
    [SerializeField] PlayerManager manager;
    [SerializeField] RectTransform hpBar, stamBar, pollutionBar, shieldBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, manager.health/manager.maxHp * 500);
        stamBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, manager.stamina / manager.maxStamina * 300);
        pollutionBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, manager.pollutionAmount / 50 * 180);

        if (manager.shieldActive) {
            if (shieldBar.gameObject.activeSelf) {
                shieldBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, manager.shield / 40 * 200);
            }
            else
            {
                shieldBar.gameObject.SetActive(true);
                shieldBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, manager.shield / 40 * 200);
            }
        }
        else shieldBar.gameObject.SetActive(false);

    }
}
