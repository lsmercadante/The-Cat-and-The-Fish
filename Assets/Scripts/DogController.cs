using UnityEngine;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        //assigns rigidbody component to variable
        spriteRenderer = visual.GetComponent<SpriteRenderer>();  //assigns spriterenderer component to variable
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        flipTimer += Time.fixedDeltaTime;
        if (flipTimer >= flipInterval)
            Flip();

        CheckSurroundings();
    }
    void Flip()
    {
        direction *= -1;
        flipTimer = 0f;                      // restart the timed patrol
        spriteRenderer.flipX = direction > 0;
    }
    void CheckSurroundings()
    {
        // point just ahead of the dog, at its feet
        Vector2 front = (Vector2)transform.position + Vector2.right * direction * frontOffset;

        // ledge check: ray down from the front point
        RaycastHit2D ground = Physics2D.Raycast(front, Vector2.down, edgeCheckDistance, groundLayer);

        // wall check: ray forward from the dog
        RaycastHit2D wall = Physics2D.Raycast(transform.position, Vector2.right * direction, wallCheckDistance, groundLayer);

        if (ground.collider == null || wall.collider != null)
            Flip();
    }
    void OnDrawGizmos()
    {
        Vector2 front = (Vector2)transform.position + Vector2.right * direction * frontOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(front, front + Vector2.down * edgeCheckDistance);           // ledge ray
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * direction * wallCheckDistance); // wall ray
    }
}
