using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerMove_Tugas : MonoBehaviour
{
    CharacterController cc;

    public float positionClamp = 5f;

    public float moveSpeed = 5f;
    private PlayerInput input;
    private InputAction moveAction;

    private Vector2 movement;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        moveAction = input.actions["Move"];
    }

    void Update()
    {
        movement = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (movement.sqrMagnitude > .001f)
        {
            Vector3 mv = (movement.x * transform.right).normalized;

            cc.Move(moveSpeed * Time.fixedDeltaTime * mv);
        }
    }
}
