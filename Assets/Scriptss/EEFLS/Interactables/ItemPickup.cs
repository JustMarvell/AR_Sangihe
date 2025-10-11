using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public GameObject itemPickupTextPrefab;

    public override void Interact()
    {
        base.Interact();

        PickUpItem();
    }

    void PickUpItem()
    {
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp) Debug.Log("Picked up " + item.name);
        else Debug.Log("Failed to pick " + item.name);

        PlayerInteraction.onInteraction = false;

        Destroy(gameObject);
    }
}
