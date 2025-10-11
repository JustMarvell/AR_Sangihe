using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    // TODO : Add GameManager/GameMaster
    // GameMaster gm;

    public Transform itemsParent;
    public GameObject inventoryUI;

    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        // gm = GameMaster.instance;
        inventory.onItemChangedCallback += UpdateUI;
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        inventory.isOpeningInventory = inventoryUI.activeSelf;
        // TODO : Trigger pause
        // gm.TriggerPause(inventoryUI.activeSelf);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.items.Count)
            {
                slots[i].AddItem(inventory.items.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
