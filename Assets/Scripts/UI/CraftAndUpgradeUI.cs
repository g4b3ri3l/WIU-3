using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftAndUpgradeUI : MonoBehaviour
{
    [SerializeField] GameObject craftPanel, upgradePanel, UI;

    public void CraftPanel()
    {
        craftPanel.SetActive(true);
        upgradePanel.SetActive(false);
    }

    public void UpgradePanel()
    {
        craftPanel.SetActive(false);
        upgradePanel.SetActive(true);
    }

    public void CancelPanels()
    {
        UI.SetActive(false);
    }

}
