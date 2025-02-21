using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    private Vector2 _moveDirection;

    public InputActionReference move;

    private void Start()
    {
        rb.isKinematic = true; //Don't fly pls :)
    }

    private void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();


        if (_moveDirection.magnitude > 0)
        {
            rb.isKinematic = false;
        }
    }

    private void FixedUpdate()
    {
        if (!rb.isKinematic)
        {
            Vector3 moveVector = new Vector3(_moveDirection.x, 0f, _moveDirection.y);
            rb.velocity = moveVector * moveSpeed + new Vector3(0, rb.velocity.y, 0);
            //Debug.Log(rb.velocity);
        }
    }
}