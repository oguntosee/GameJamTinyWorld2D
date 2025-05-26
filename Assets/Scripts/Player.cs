using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator anim;
    public bool facingRight = true;

    [Header("Collision Check")]
    public LayerMask whatIsGround; // ground
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public bool isGrounded;

    [Header("Movement")]
    public float xInput;
    public bool isMoving;
    public float moveSpeed = 10.0f;
    public float jumpForce = 4f;

    [Header("Snowball attack")]
    public GameObject snowballPrefab;
    public Transform snowballSpawnPoint;
    public float snowballSpeed = 10f;

    [Header("Sounds")]
    public AudioSource footstepAudioSource;
    public AudioClip[] footstepSound;

    [Header("Progress Bar Location")]
    public GameObject progressBarPrefab;
    public GameObject currentBar;

    // Input Actions
    public PlayerInput playerInput;
    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction throwAction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Input System setup
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
            throwAction = playerInput.actions["Throw"];
        }

        GameSceneManager.Instance.AssignPlayer(this);
    }

    void Update()
    {
        CollisionChecks();
        Idle();
        Movement();
        HandleInput();
    }

    private void HandleInput()
    {
        // Movement input
        if (moveAction != null)
        {
            xInput = moveAction.ReadValue<Vector2>().x;
        }
        else
        {
            // Fallback to old input system
            xInput = Input.GetAxisRaw("Horizontal");
        }

        // Jump input
        if (jumpAction != null && jumpAction.WasPressedThisFrame())
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Throw input
        if (throwAction != null && throwAction.WasPressedThisFrame() && isMoving)
        {
            ThrowSnowball();
        }
        else if (Input.GetKeyDown(KeyCode.F) && isMoving)
        {
            ThrowSnowball();
        }

        // Handle flipping
        if (xInput > 0 && !facingRight)
            Flip();
        else if (xInput < 0 && facingRight)
            Flip();
    }

    private void Idle()
    {
        isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        anim.SetBool("isMoving", isMoving);
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void ThrowSnowball()
    {
        if (snowballPrefab == null) return;

        Vector3 positionOfPlayer = this.gameObject.transform.position;
        GameObject snowball = Instantiate(snowballPrefab, positionOfPlayer, Quaternion.identity);
        Rigidbody2D snowballRb = snowball.GetComponent<Rigidbody2D>();

        if (snowballRb != null)
        {
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            snowballRb.AddForce(direction * snowballSpeed, ForceMode2D.Impulse);
            DestroySnowBall(snowball);

            Collider2D snowballCol = snowball.GetComponent<Collider2D>();
            Collider2D playerCol = GetComponent<Collider2D>();
            if (snowballCol != null && playerCol != null)
            {
                Physics2D.IgnoreCollision(snowballCol, playerCol);
            }
        }
    }

    public void PlayFootstepSound()
    {
        if (footstepSound != null && footstepAudioSource != null && footstepSound.Length > 0)
        {
            footstepAudioSource.clip = footstepSound[Random.Range(0, footstepSound.Length)];
            footstepAudioSource.Play();
        }
    }

    private void DestroySnowBall(GameObject snowball)
    {
        Destroy(snowball, 5f);
    }
}