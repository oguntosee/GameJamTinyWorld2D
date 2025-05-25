using UnityEditor.SearchService;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = true;
    [Header("Collision Check")]
    [SerializeField] private LayerMask whatIsGround; // ground
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private bool isGrounded;


    [Header("Movement")]
    public float xInput;
    [SerializeField] private bool isMoving;
    [SerializeField] public float moveSpeed = 10.0f;
    [SerializeField] private float jumpForce = 4f;

    [Header("Snowball attack")]
    public GameObject snowballPrefab;
    public Transform snowballSpawnPoint;
    public float snowballSpeed = 10f;

    [Header("Sounds")]
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip[] footstepSound;

    [Header("Progress Bar Location")]
    public GameObject progressBarPrefab;
    public GameObject currentBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); //anim
        GameSceneManager.Instance.AssignPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        CollisionChecks();

        Idle();
        Movement();
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (xInput > 0 && !facingRight)
            Flip();
        else if (xInput < 0 && facingRight)
            Flip();

        if (isMoving && Input.GetKeyDown(KeyCode.F))
            ThrowSnowball();

    }

    private void Idle()
    {
        isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);
    }
    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        //transform = 180 (y)
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
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }


    private void ThrowSnowball()
    {

        Vector3 positionOfPlayer = this.gameObject.transform.position;
        GameObject snowball = Instantiate(snowballPrefab, positionOfPlayer, Quaternion.identity);
        Rigidbody2D snowballRb = snowball.GetComponent<Rigidbody2D>();

        if (snowballRb != null)
        {
            //Vector2 temp = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
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
        if (footstepSound != null && footstepAudioSource != null)
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
