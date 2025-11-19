using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPickerSlot : MonoBehaviour, IPointerClickHandler
{
    public Item item;

    public void OnPointerClick(PointerEventData eventData)
    {
        // pick object
        ObjectPickerUI.instance.SelectItem(item);
    }
}
