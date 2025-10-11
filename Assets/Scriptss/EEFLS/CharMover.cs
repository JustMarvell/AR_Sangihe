using UnityEngine;

public class CharMover : MonoBehaviour
{
    public float speed = 5f;

    private CharacterController controller;

    private InputManager3D input;

    private Vector2 movement => input.moveInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputManager3D>();
    }

    void FixedUpdate()
    {
        if (movement != null)
        {
            Vector3 direction = (transform.forward * movement.y + transform.right * movement.x).normalized;

            controller.Move(speed * Time.fixedDeltaTime * direction);
        }
    }
}
