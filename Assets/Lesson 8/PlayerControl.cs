using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    private Vector2 _moveDirection;

    public InputActionReference move;

    private void Update() => _moveDirection = move.action.ReadValue<Vector2>();

    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(_moveDirection.x, 0f, _moveDirection.y);
        rb.velocity = moveVector * moveSpeed + new Vector3(0, rb.velocity.y, 0);
        //Debug.Log(rb.velocity);
    }
}
