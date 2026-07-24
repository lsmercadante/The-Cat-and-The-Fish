using UnityEngine;
using System.Collections;
using System.Reflection;

public class DogController : MonoBehaviour
{
    // Basic variables for tracking movement
    Rigidbody2D rb;
    public float speed = 3.0f;
    int direction = -1;
    public Transform visual;
    SpriteRenderer spriteRenderer;


    public float flipInterval = 3f;     // seconds before it turns around on its own
    public float frontOffset = 0.4f;    // how far ahead the edge/wall checks sit
    public float edgeCheckDistance = 0.6f;
    public float wallCheckDistance = 0.3f;
    public LayerMask groundLayer;
    float flipTimer;

    public bool isKnockedOut;
    public float knockoutTime = 3f;
    public float idleTime = 0.4f;

    Animator animator;

    [SerializeField] ParticleSystem stompParticles; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        //assigns rigidbody component to variable
        spriteRenderer = visual.GetComponent<SpriteRenderer>();  //assigns spriterenderer component to variable
        animator = visual.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isKnockedOut)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); // hold still, keep gravity
            return;                                                    // skip patrol + checks
        }
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);    // sets the dogs velocity to the speed
        flipTimer += Time.fixedDeltaTime;       // timer counting up to flip time
        if (flipTimer >= flipInterval)          // when flip time is reached, flip occurs
            Flip();

        CheckSurroundings();        // checks for ledges and walls
    }
    void Flip()
    {
        direction *= -1;                    // changes direction
        flipTimer = 0f;                      // restart the timed patrol
        spriteRenderer.flipX = direction > 0;   // flips sprite direction
    }
    void CheckSurroundings()
    {
        // point just ahead of the dog, at its feet
        Vector2 front = (Vector2)transform.position + Vector2.right * direction * frontOffset;

        // ledge check: ray down from the front point
        RaycastHit2D ground = Physics2D.Raycast(front, Vector2.down, edgeCheckDistance, groundLayer);

        // wall check: ray forward from the dog
        RaycastHit2D wall = Physics2D.Raycast(transform.position, Vector2.right * direction, wallCheckDistance, groundLayer);

        if (ground.collider == null || wall.collider != null) // flips if wall is there or ground is not there
            Flip();
    }
    void OnDrawGizmos()
    {
        Vector2 front = (Vector2)transform.position + Vector2.right * direction * frontOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(front, front + Vector2.down * edgeCheckDistance);           // ledge ray
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * direction * wallCheckDistance); // wall ray
    }

    public void Defeat()
    {
        if (isKnockedOut) return;        // already down, ignore repeat hits
        stompParticles.Play();
        StartCoroutine(KnockoutRoutine());
    }

    IEnumerator KnockoutRoutine()
    {
        isKnockedOut = true;
        rb.linearVelocity = Vector2.zero;                  // stop moving
        rb.bodyType = RigidbodyType2D.Kinematic;   // can't be shoved while dazed
        animator.SetTrigger("Dazed");        // Walk -> Dazed

        yield return new WaitForSeconds(knockoutTime);     // stay down

        rb.bodyType = RigidbodyType2D.Dynamic;     // back to normal patrol physics
        animator.SetTrigger("Recover");      // Dazed -> Idle
        yield return new WaitForSeconds(idleTime);
                                    
        animator.SetTrigger("Walk"); 
        isKnockedOut = false;  // resume patrol
    }
}
