using UnityEngine.UI;
using UnityEngine;

public class itemslot2 : MonoBehaviour
{

    [SerializeField] Item item;
    [SerializeField] GameObject disabled;
    private TMPro.TMP_Text text;


    private void Start()
    {
        text = GetComponentInChildren<TMPro.TMP_Text>();
    }
    void Update()
    {
        if (item != null)
        {
            text.text = "x " + item.itemCount;

            if (item.itemCount == 0)
            {
                disabled.SetActive(true);
            }
            else disabled.SetActive(false);
        }
    }
    public void DisplayItem()
    {
        DescriptionUI.instance.item = item;
        DescriptionUI.instance.UpdateUI();
    }

}
