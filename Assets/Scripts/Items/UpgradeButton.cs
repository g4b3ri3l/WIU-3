using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Item[] materials;
    [SerializeField] private int[] materialsNeeded;
    [SerializeField] GameObject disabler;
    private Button buttonUpgrade;
    private int upgradeCheck = 0;
    private bool upgradecheckbool = false;
    private void Start()
    {
        buttonUpgrade = GetComponent<Button>();
    }
    void Update()
    {
        upgradecheckbool = true;
        for (int i = 0; i < materials.Length; i++) {
            if (materials[i].itemCount < materialsNeeded[i])
            {
                upgradeCheck += 1;
            }
        }

        if (upgradeCheck > 0)
        {
            upgradecheckbool = false;      
        }

        disabler.SetActive(!upgradecheckbool);
        buttonUpgrade.interactable = upgradecheckbool;

        upgradeCheck = 0;
    }

    public void Upgrade()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].itemCount -= materialsNeeded[i];
        }
    }
}
