using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public GameObject itemPrefab;
    public Sprite icon = null;
    public bool isDefaultItem = false;

    [Space]

    [TextArea(3, 10)] public string itemDescription;

    public virtual void Use()
    {
        Debug.Log("Using : " + itemName);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
