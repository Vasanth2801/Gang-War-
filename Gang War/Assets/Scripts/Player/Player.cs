using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private int facingDirection = 1;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private bool isGrounded = false;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private LayerMask attackLayerMask;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [Header("Input Settings")]
    [SerializeField] private float moveInput;

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(moveInput > 0 && transform.localScale.x < 0 || moveInput < 0 && transform.localScale.x  > 0)
        {
            Flip();
        }

        HandleAnimations();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        PunchAttack();
        KickAttack();
        DoubleKickAttack();
        SmashAttack();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius,groundLayerMask);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void PunchAttack()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Punch");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackLayerMask);
            foreach(Collider2D hit in hitEnemies)
            {
                EnemyHealth eh = hit.GetComponent<EnemyHealth>();
                if(eh != null)
                {
                    eh.TakeDamage(10);
                }
            }
        }
    }

    void KickAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Kick");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackLayerMask);
            foreach (Collider2D hit in hitEnemies)
            {
                EnemyHealth eh = hit.GetComponent<EnemyHealth>();
                if (eh != null)
                {
                    eh.TakeDamage(12);
                }
            }
        }
    }

    void DoubleKickAttack()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("DoubleKick");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackLayerMask);
            foreach (Collider2D hit in hitEnemies)
            {
                EnemyHealth eh = hit.GetComponent<EnemyHealth>();
                if (eh != null)
                {
                    eh.TakeDamage(15);
                }
            }
        }
    }

    void SmashAttack()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("Smash");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackLayerMask);
            foreach (Collider2D hit in hitEnemies)
            {
                EnemyHealth eh = hit.GetComponent<EnemyHealth>();
                if (eh != null)
                {
                    eh.TakeDamage(20);
                }
            }
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void HandleAnimations()
    {
        animator.SetFloat("Speed",Mathf.Abs(moveInput));
    }
}