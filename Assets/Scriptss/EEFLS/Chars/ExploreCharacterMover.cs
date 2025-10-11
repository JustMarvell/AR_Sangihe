using UnityEngine;

public class ExploreCharacterMover : MonoBehaviour
{
    private Transform cam;
    private Vector3 direction;
    private CharacterController controller;
    private VirtualJoystick joystick;
    private float rotationVelocity;

    public float speed = 5f;
    public float rotationSmoothTime = .1f;

    void Awake()
    {
        cam = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        joystick = VirtualJoystick.instance;
    }

    void FixedUpdate()
    {
        if (joystick.direction != Vector2.zero)
        {
            Vector3 inputDir = cam.forward * joystick.direction.y + cam.right * joystick.direction.x;
            inputDir.y = 0f;

            direction = inputDir.normalized * speed;

            RotateCharacter(inputDir);

            controller.Move(direction * Time.fixedDeltaTime);
        }
    }

    void RotateCharacter(Vector3 moveDir)
    {
        if (moveDir.sqrMagnitude < .001f)
            return;

        float targetAngle = Mathf.Atan2(joystick.direction.x, joystick.direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
    }
}
