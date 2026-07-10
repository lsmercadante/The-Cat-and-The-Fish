using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public InputAction JumpAction;
    Rigidbody2D rigidbody2d;

    Vector2 move;
    bool jumpRequested;
    public float speed = 3.0f;
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

    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        JumpAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        move = MoveAction.ReadValue<Vector2>();
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

        }
        if (JumpAction.WasReleasedThisFrame() && rigidbody2d.linearVelocity.y > 0f)
        {
            rigidbody2d.linearVelocity = new Vector2(
                rigidbody2d.linearVelocity.x,
                rigidbody2d.linearVelocity.y * jumpCutMultiplier
            );
        }

        if (move.x != 0)
        {
            spriteRenderer.flipX = move.x > 0;
        }
        // if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        // {
        //     moveDirection.Set(move.x, move.y);
        //    moveDirection.Normalize();
        //}
    }

    void FixedUpdate()
    {
        rigidbody2d.linearVelocity = new Vector2(move.x * speed, rigidbody2d.linearVelocity.y);
        if (jumpRequested)
        {
            rigidbody2d.linearVelocity = new Vector2(rigidbody2d.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }

    }
    void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}
