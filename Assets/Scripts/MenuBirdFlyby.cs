using UnityEngine;

public class MenuBirdFlyby : MonoBehaviour
{
    public float speed = 2f;
    public float bobHeight = 0.5f;
    public float bobSpeed = 2f;
    public float destroyXBound = 15f; // world-space X past which the bird despawns
    Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        float bob = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, startPos.y + bob, transform.position.z);

        if (Mathf.Abs(transform.position.x) > destroyXBound)
            Destroy(gameObject);

    }
}
