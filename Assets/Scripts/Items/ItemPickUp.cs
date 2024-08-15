using UnityEngine;

public class ItemPickUp : Interactable
{

    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Pick Up " + item.name);

        //Add item into inventory
        bool success = Inventory.instance.Add(item);
        if (success)
        {
            Destroy(gameObject);
        }
    }
}
