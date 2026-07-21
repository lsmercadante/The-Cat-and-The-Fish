using UnityEngine;

public class BlueJayController : MonoBehaviour
{
    // Basic variables for tracking movement
    Rigidbody2D rb;
    public float speed = 3.0f;
    int direction = -1;
    public Transform visual;
    SpriteRenderer spriteRenderer;
    public float flipInterval = 3f;     // seconds before it turns around on its own
    float flipTimer;

     Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        //assigns rigidbody component to variable
        spriteRenderer = visual.GetComponent<SpriteRenderer>();  //assigns spriterenderer component to variable
        animator = visual.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);    // sets the birds velocity to the speed
        flipTimer += Time.fixedDeltaTime;       // timer counting up to flip time
        if (flipTimer >= flipInterval)          // when flip time is reached, flip occurs
            Flip();
    }

    void Flip()
    {
        direction *= -1;                    // changes direction
        flipTimer = 0f;                      // restart the timed patrol
        spriteRenderer.flipX = direction > 0;   // flips sprite direction
    }
}
