using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Input Settings")]
    [SerializeField] private float moveInput;

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }
}
