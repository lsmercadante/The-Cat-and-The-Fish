using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;   // CM3 namespace

public class PlayerController : MonoBehaviour
{
    //Input actions for controlling player movement
    public InputAction MoveAction;
    public InputAction JumpAction;

    // Basic variables for tracking movement
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;

    //Variables for jump
    bool jumpRequested;
    public float jumpForce = 7.0f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    bool isGrounded;
    public float coyoteTime = 0.12f;
    float coyoteTimer;
    public float jumpBufferTime = 0.1f;
    float jumpBufferTimer;
    public float jumpCutMultiplier = 0.5f;
    public Transform visual;
    Vector3 squashScale = Vector3.one;
    public float squashSpeed = 12f;
    bool wasGrounded;

    //Variables for sprite rendering/animations
    SpriteRenderer spriteRenderer;
    Animator animator;

    CinemachineImpulseSource impulseSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();        // Enables action inputs
        JumpAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();        //assigns rigidbody component to variable
        spriteRenderer = visual.GetComponent<SpriteRenderer>();  //assigns spriterenderer component to variable
        animator = visual.GetComponent<Animator>();              // assigns animator component to variable
        impulseSource = GetComponent<CinemachineImpulseSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // Creating the overlap circle to check for overlap between cat feet position and ground layers
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        move = MoveAction.ReadValue<Vector2>();            //reads vector value of movement input
        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }     // reset while on ground
        else
        {
            coyoteTimer -= Time.deltaTime;
        } // count down once you're in air

        // jump buffer
        if (JumpAction.WasPressedThisFrame())
        {
            jumpBufferTimer = jumpBufferTime;   // remember the press
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;  // forget it after the window
        }
        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            jumpRequested = true; // don't want jump press to be missed so put it here
            coyoteTimer = 0f;   // consume it so you can't jump twice off one ledge
            jumpBufferTimer = 0f;   // consume the press
            squashScale = new Vector3(0.8f, 1.2f, 1f);   // stretch on jump

        }
        // Variable jump heights
        if (JumpAction.WasReleasedThisFrame() && rigidbody2d.linearVelocity.y > 0f)
        {
            rigidbody2d.linearVelocity = new Vector2(
                rigidbody2d.linearVelocity.x,
                rigidbody2d.linearVelocity.y * jumpCutMultiplier
            );
        }
        // Sprite flipping when direction changes
        if (move.x != 0)
        {
            spriteRenderer.flipX = move.x > 0;
        }
        // landing detection
        if (isGrounded && !wasGrounded)
            squashScale = new Vector3(1.2f, 0.8f, 1f);   // squash on land
        wasGrounded = isGrounded;

        // ease back to normal and apply to the visual only
        squashScale = Vector3.Lerp(squashScale, Vector3.one, Time.deltaTime * squashSpeed);
        visual.localScale = squashScale;

        //Animating the movement
        animator.SetBool("isRunning", Mathf.Abs(move.x) > 0.1f);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rigidbody2d.linearVelocity.y);
    }

    void FixedUpdate()
    {
        //Updating the velocity of the player 
        rigidbody2d.linearVelocity = new Vector2(move.x * speed, rigidbody2d.linearVelocity.y);
        // Implementing the jump
        if (jumpRequested)
        {
            rigidbody2d.linearVelocity = new Vector2(rigidbody2d.linearVelocity.x, jumpForce);
            jumpRequested = false;

        }

    }

    void OnDrawGizmos()
    // for visualization of the ground overlap circle
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    void Shake(float force = 1f)
    {
        impulseSource.GenerateImpulse(force);
    }

}
