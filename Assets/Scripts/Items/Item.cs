using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description = "An Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int itemCount = 0;

    public virtual void Use()
    {
        Debug.Log("Using item: " + name);
    }

}
