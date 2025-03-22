using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float gravity = -9.81f; 
    [SerializeField] private float jumpHeight = 2f; 
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Push Settings")]
    [SerializeField] private float pushPower = 2f;

    private CharacterController controller;
    private Vector3 velocity; // Character speed
    private bool isGrounded; // Is the character on the ground?
    private PlayerInputActions playerInputActions; 

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    void Update()
    {
        // Checking if a character is on the ground
        CheckGrounded();

        Vector2 moveInput = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y; 
        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded && playerInputActions.Player.Jump.triggered)
        {
            velocity.y = Mathf.Sqrt(2 * jumpHeight * -gravity);
        }

        // Application of gravity
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime; // Apply gravity if the character is not on the ground
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f; // We reset the speed if the character is on the ground
        }

        controller.Move(velocity * Time.deltaTime); // We apply vertical speed
    }

    void CheckGrounded()
    {
        // Using SphereCast to check the ground
        RaycastHit hit;
        isGrounded = Physics.SphereCast(
            transform.position, // Starting position
            controller.radius,  // Capsule radius
            Vector3.down,       // Inspection direction
            out hit,            // Verification result
            groundCheckDistance + controller.skinWidth, // Inspection distance
            groundLayer         // Layer for verification
        );

        // If the character is on the ground, reset the vertical speed
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Additional debug: Raycast visualization
        Debug.DrawRay(transform.position, Vector3.down * (groundCheckDistance + controller.skinWidth), Color.red);
    }

    // Interacting with objects that have Rigidbody
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // If the character collides with an object that has a Rigidbody
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            // Push the object aside to avoid "lifting" the character
            Vector3 pushDirection = hit.moveDirection;
            pushDirection.y = 0; // Ignore vertical motion
            body.AddForce(pushDirection * pushPower, ForceMode.Impulse);

            // Additionally: reduce the player's vertical speed
            velocity.y = -2f;
        }
    }
}