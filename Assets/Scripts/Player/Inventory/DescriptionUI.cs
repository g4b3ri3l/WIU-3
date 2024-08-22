using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionUI : MonoBehaviour
{
    #region Singleton
    public static DescriptionUI instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Item item;
    [SerializeField] public TextMeshProUGUI description;
    [SerializeField] public new TextMeshProUGUI name;
    [SerializeField] public Image icon;

    public void UpdateUI()
    {
        description.text = item.description;
        name.text = item.name;
        icon.sprite = item.icon;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
