using UnityEngine;
using System.Collections;
using UnityEditor;

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

    private bool isFacingRight = true;
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
        if(PlayerDash.instance.isDashing)
        {
            return;
        }
        
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        MovePlayer(horizontalMovement, verticalMovement);

        float characterVelocity = Mathf.Abs(rb.linearVelocity.x);
        if (isGrounded())
        {
            animator.SetFloat("Speed", characterVelocity);
        }
        else 
        {
            animator.SetBool("Jump", true);
        }
        
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, collisionLayers);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
            if (!isClimbing)
            {
                
                Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.linearVelocity.y);
                rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);

                if (isJumping)
                {
                    rb.AddForce(new Vector2(0f, jumpForce));
                    isJumping = false;
                }
            }
            else
            {
                
                Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);
                rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);
                if (isJumping)
                {
                    rb.AddForce(new Vector2(0f, jumpForce));
                    isJumping = false;
                }
            }
        
    }

    void Update()
    {
        Flip();
        if(PlayerDash.instance.isDashing)
        {
            return;
        }
        if (doubleJumpPowerUp)
        {
            PowerUpSkin.instance.skinNr = 1;
            if(isGrounded() && doubleJump==0)
            {
                doubleJump = 1;
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded())
                {
                    Audio_Manager.instance.PlayClipAt(jumpSound, transform.position);
                    isJumping = true;
                }
                if (!isGrounded() && doubleJump == 1)
                {
                    Audio_Manager.instance.PlayClipAt(jumpSound, transform.position);
                    rb.linearVelocity = Vector3.zero;
                    rb.AddForce(new Vector2(0f, doubleJumpForce));
                    doubleJump = 0;
                }

            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded()) 
                {
                    Audio_Manager.instance.PlayClipAt(jumpSound, transform.position);
                    isJumping = true;
                }
            }

        }
        if (animator.GetBool("Jump")==true && isGrounded() == true)
        {
            animator.SetBool("Jump", false);
            animator.SetTrigger("onGround");
            
        }
        
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeakSpot"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Trampoline"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(new Vector2(0f, trampoJumpForce));
        }
    }


    void Flip()
    {
        if(isFacingRight && horizontalMovement <0f || !isFacingRight && horizontalMovement > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

    }
}

