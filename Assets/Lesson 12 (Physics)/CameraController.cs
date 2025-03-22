using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target; // Character followed by camera
    public float distance = 5f; // Camera distance from character
    public float sensitivity = 100f; // Mouse sensitivity

    private float xRotation = 0f; // Camera rotation vertically
    private float yRotation = 0f; // Horizontal camera rotation
    private PlayerInputActions playerInputActions; 

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        // enable actions
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        // Disable actions
        playerInputActions.Player.Disable();
    }

    void Update()
    {
        // Check if the right mouse button is pressed
        if (playerInputActions.Player.Look.IsPressed())
        {
            Vector2 lookInput = Mouse.current.delta.ReadValue();
            float mouseX = lookInput.x * sensitivity * Time.deltaTime;
            float mouseY = lookInput.y * sensitivity * Time.deltaTime;

            // Camera rotation
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Vertical rotation limitation

            // Applying camera rotation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            transform.position = target.position - transform.forward * distance;

            // The character returns with the camera
            target.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}