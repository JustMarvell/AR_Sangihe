using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int space = 5;
    public static Inventory instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public bool isOpeningInventory = false;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than 1 inventory instance is detected");
        }
        instance = this;
    }

    // public List<Item> items = new();
    public InventoryObject items;

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.items.Count >= space)
            {
                Debug.Log("Inventory Full");
                return false;
            }

            items.items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }

    /// <summary>
    /// Check if there is a specified item in inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CheckItem(Item item)
    {
        foreach (Item _item in items.items)
        {
            if (_item.name == item.name)
            {
                return true;
            }
        }
        
        return false;
    }

    public Item GetItem(Item item)
    {
        return items.items.Find(_item => _item.name == item.name);
    }

    public void Remove(Item item)
    {
        items.items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
