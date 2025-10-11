using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public string inventoryName = "Player Inventory";

    public List<Item> items;
}
