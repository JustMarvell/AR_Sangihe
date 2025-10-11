using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static VirtualJoystick instance;

    void Awake()
    {
        instance = this;
    }

    public RectTransform background, handle;
    public float handleRange = 50f;
    public Vector2 input = Vector2.zero;
    public Vector2 direction => input;

    public void OnBeginDrag(PointerEventData eventData) => OnDrag(eventData);

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out Vector2 pos);
        pos = Vector2.ClampMagnitude(pos, handleRange);
        handle.anchoredPosition = pos;
        input = pos / handleRange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
