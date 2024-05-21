using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float rotationSpeed = 15.0f; // Rotation speed factor

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Get input from user
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Set movement direction
            moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);

            if (moveDirection.magnitude > 0)
            {
                // Calculate the target angle based on input movement
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);

                // Set character rotation
                transform.rotation = Quaternion.Euler(0, angle, 0);

                // Set movement direction relative to rotation
                moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * speed;
            }

            // Jump if the jump button is pressed
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity manually
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        controller.Move(moveDirection * Time.deltaTime);
    }
}
