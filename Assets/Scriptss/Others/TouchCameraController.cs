using UnityEngine;
using UnityEngine.EventSystems;

public class TouchCameraController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Camera Settings")]
    public Transform target; // The player or target to orbit around (e.g., your character's transform)
    public Transform mainCamera; // Reference to the main camera
    public float distance = 5f; // Initial distance from the target
    public float sensitivityX = 5f; // Horizontal sensitivity
    public float sensitivityY = 2f; // Vertical sensitivity (lower to prevent over-tilting)
    public float minPitch = -60f; // Minimum vertical angle
    public float maxPitch = 60f; // Maximum vertical angle
    public float smoothing = 5f; // Smoothing factor for camera movement

    private float currentYaw = 0f;
    private float currentPitch = 0f;
    private Vector2 touchStartPos;
    private bool isDragging = false;

    private Quaternion targetRotation;
    private Vector3 targetPosition;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }

        // Initialize current yaw and pitch based on initial camera orientation
        Vector3 direction = mainCamera.position - target.position;
        direction.Normalize();
        currentPitch = Mathf.Asin(direction.y) * Mathf.Rad2Deg;
        currentYaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        UpdateCameraPositionAndRotation(true); // Immediate update
    }

    void Update()
    {
        if (!isDragging)
        {
            // Smoothly interpolate even when not dragging for any lingering momentum (optional, but adds natural feel)
            // mainCamera.position = Vector3.Lerp(mainCamera.position, targetPosition, Time.deltaTime * smoothing);
            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, targetRotation, Time.deltaTime * smoothing);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        touchStartPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 delta = eventData.position - touchStartPos;
        touchStartPos = eventData.position;

        // Adjust yaw and pitch based on drag delta (inverted Y for natural feel like in mobile games)
        currentYaw += delta.x * sensitivityX * Time.deltaTime;
        currentPitch -= delta.y * sensitivityY * Time.deltaTime;

        // Clamp pitch to prevent camera flipping
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        UpdateCameraPositionAndRotation(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void UpdateCameraPositionAndRotation(bool immediate)
    {
        // Calculate the desired rotation
        targetRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);

        // Calculate the desired position (orbit around target)
        // targetPosition = target.position - targetRotation * Vector3.forward * distance;

        if (immediate)
        {
            // mainCamera.position = targetPosition;
            mainCamera.rotation = targetRotation;
        }
        else
        {
            // During drag, update with smoothing for natural feel
            // mainCamera.position = Vector3.Lerp(mainCamera.position, targetPosition, Time.deltaTime * smoothing);
            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, targetRotation, Time.deltaTime * smoothing);
        }

        // Ensure the camera always looks at the target
        // mainCamera.LookAt(target.position);
    }
}