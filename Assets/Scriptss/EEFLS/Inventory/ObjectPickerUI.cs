using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPickerUI : MonoBehaviour
{
    public static ObjectPickerUI instance;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public Transform objectSlot;
    public GameObject slotPrefab;
    List<ObjectPickerSlot> slots;

    [Space]

    public Item currentSelectedItem;

    [Space]

    public TextMeshProUGUI itemNamePlate;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Image itemImage;

    private const string itemNameStartSentence = "";
    private const string itemDescriptionStartSentence = "";

    public bool addedAllSlots = false;
    public float progress = 0;

    void Start()
    {
        slots = new();
        
        StartCoroutine(AddToSlot());
    }


    public IEnumerator AddToSlot()
    {
        progress = 0f;

        while (!addedAllSlots)
        {
            for (int i = 0; i < Inventory.instance.items.items.Count; i++)
            {
                AddToSlot_(Inventory.instance.items.items[i]);
                progress = i + 1 / Inventory.instance.items.items.Count;
                yield return null;
            }
            addedAllSlots = true;
        }

        SelectItem(slots[0].item);
    }

    public void AddToSlot_(Item item)
    {
        // instantiate slot object
        GameObject slotObj = Instantiate(slotPrefab, objectSlot);

        // assign all variables inside slot object
        ObjectPickerSlot slot = slotObj.GetComponent<ObjectPickerSlot>();
        slot.item = item;
        slotObj.transform.GetChild(0).GetComponent<Image>().sprite = item.icon; 

        // add slot object to slots list
        slots.Add(slot);
    }
    
    public void SelectItem(Item selectedItem)
    {
        if (currentSelectedItem == selectedItem)
            return;

        currentSelectedItem = selectedItem;
        
        // assign value
        itemNamePlate.text = currentSelectedItem.itemName;
        itemName.text = itemNameStartSentence + currentSelectedItem.itemName;
        itemDescription.text = itemDescriptionStartSentence + currentSelectedItem.itemDescription;
        itemImage.sprite = (currentSelectedItem.realImage == null) ? currentSelectedItem.icon : currentSelectedItem.realImage;
    }
}
