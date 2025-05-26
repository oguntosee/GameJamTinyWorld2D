using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    
    [Header("Collision Check")]
    private LayerMask whatIsGround;
    private Transform groundCheck;
    private float groundCheckRadius;
    private bool isGrounded;


    [Header("Movement")]
    public float xInput;
    private float moveSpeed = 2.0f;
    private float jumpForce = 4f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        CollisionChecks();

        xInput = Input.GetAxisRaw("Horizontal");

        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();


    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }


    }

    private void Movement()
    {
        rb.linearVelocity = new Vector2(moveSpeed * xInput, rb.linearVelocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
