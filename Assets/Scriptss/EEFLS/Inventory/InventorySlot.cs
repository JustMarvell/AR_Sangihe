using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerDownHandler
{
    Item item;
    public Image icon;
    public Button removeButton;

    public string targetTag = "ItemDescriptionUI";
    private GameObject itemDescriptionParent;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    void Start()
    {
        itemDescriptionParent = GameObject.FindGameObjectWithTag(targetTag);
        itemName = itemDescriptionParent.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        itemDescription = itemDescriptionParent.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            
            itemDescriptionParent.GetComponent<CanvasGroup>().alpha = 0;
            itemName.text = "";
            itemDescription.text = "";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (icon.enabled)
        {
            itemDescriptionParent.GetComponent<CanvasGroup>().alpha = 1;
            itemName.text = item.name;
            itemDescription.text = item.itemDescription;

            if (eventData.pointerPress)
            {
                itemDescriptionParent.GetComponent<CanvasGroup>().alpha = 0;
                itemName.text = "";
                itemDescription.text = "";
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        itemDescriptionParent.GetComponent<CanvasGroup>().alpha = 0;
        itemName.text = "";
        itemDescription.text = "";
    }

    public void OnPointerClick(PointerEventData eventData) {
        itemDescriptionParent.GetComponent<CanvasGroup>().alpha = 0;
        itemName.text = "";
        itemDescription.text = "";

        if (eventData.pointerPress)
        {
            itemDescriptionParent.GetComponent<CanvasGroup>().alpha = 0;
            itemName.text = "";
            itemDescription.text = "";
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        itemDescriptionParent.GetComponent<CanvasGroup>().alpha = 0;
        itemName.text = "";
        itemDescription.text = "";

        if (eventData.pointerPress)
        {
            itemDescriptionParent.GetComponent<CanvasGroup>().alpha = 0;
            itemName.text = "";
            itemDescription.text = "";
        }
    }
}
