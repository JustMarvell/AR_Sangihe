using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager3D : MonoBehaviour
{
    private PlayerInput input;

    public InputAction moveAction;

    public Vector2 moveInput;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        moveAction = input.actions["Move"];
        moveAction.Enable();
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }
}
