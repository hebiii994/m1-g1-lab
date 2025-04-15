using UnityEngine;

// Automatically adds CharacterController if it's missing
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;         // Speed of horizontal movement
    public float gravity = -19.62f;        // Gravity force ( doubled -9.81 for a slightly weightier feel, adjust as needed)
    public float jumpHeight = 1.5f;        // How high the player can jump

    private CharacterController characterController;
    private Vector3 playerVelocity;        // Stores current vertical velocity (for gravity and jumping)
    private bool isGrounded;               // Tracks if the player is on the ground

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on this GameObject.", this);
            // Disable this script if the component is missing to prevent errors
            this.enabled = false;
        }
    }

    void Update()
    {
        // --- Ground Check ---
        // Check if the controller is touching the ground
        isGrounded = characterController.isGrounded;

        // If grounded and falling, reset vertical velocity to a small negative value
        // This helps ensure the controller stays firmly grounded
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // --- Horizontal Movement (WASD) ---
        // Get input values from the legacy Input Manager (Horizontal = A/D, Vertical = W/S)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on the object's local orientation
        // transform.right is the object's local X-axis (sideways)
        // transform.forward is the object's local Z-axis (forward)
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;

        // Apply movement speed and Time.deltaTime to make movement frame-rate independent
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // --- Jumping ---
        // Check for Jump input (Spacebar by default) and if grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate the upward velocity needed to reach the desired jumpHeight
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // --- Gravity ---
        // Apply gravity constantly. Multiplied by Time.deltaTime once here...
        playerVelocity.y += gravity * Time.deltaTime;

        // ...and again here when calling Move(). This ensures correct physics calculation.
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}