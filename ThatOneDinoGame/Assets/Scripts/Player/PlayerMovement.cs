using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;
    public float trampoJumpForce;

    //PowerUp

    public bool doubleJumpPowerUp = false;
    public float doubleJumpForce;


    public bool isJumping;
    private bool isGrounded;
    private int doubleJump = 1;
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public CapsuleCollider2D playerCollider;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    private float horizontalMovement;
    private float verticalMovement;

    public AudioClip jumpSound;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scene.");
            return;
        }
        instance = this;
    }


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        MovePlayer(horizontalMovement, verticalMovement);

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        if (isGrounded)
        {
            animator.SetFloat("Speed", characterVelocity);
            animator.SetBool("Jump", false);
        }
        else 
        {
            animator.SetBool("Jump", true);
        }
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {

        if (!isClimbing)
        {
            rb.gravityScale = 1;
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else
        {
            rb.gravityScale = 0;
            Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        
    }

    void Update()
    {
        if (doubleJumpPowerUp)
        {
            if(isGrounded && doubleJump==0)
            {
                doubleJump = 1;
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    Audio_Manager.instance.PlayClipAt(jumpSound, transform.position);
                    isJumping = true;
                }
                if (!isGrounded && doubleJump == 1)
                {
                    Audio_Manager.instance.PlayClipAt(jumpSound, transform.position);
                    rb.velocity = Vector3.zero;
                    rb.AddForce(new Vector2(0f, doubleJumpForce));
                    doubleJump = 0;
                }

            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    Audio_Manager.instance.PlayClipAt(jumpSound, transform.position);
                    isJumping = true;
                }
            }

        }
        
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeakSpot"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Trampoline"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(0f, trampoJumpForce));
        }
    }


    void Flip(float _velocity)
    {
        if(_velocity> 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

    }
}

