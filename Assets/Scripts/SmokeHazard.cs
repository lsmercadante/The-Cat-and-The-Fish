using UnityEngine;
/// A smoke/vent hazard that is only lethal during part of its 4-frame animation.

public class SmokeHazard : MonoBehaviour
{
    private bool isDeadly = false;              // starts safe on frame 1
    private PlayerController playerInside = null;

    //Called by Animation Events on the smoke clip
    public void SetDeadly()
    { isDeadly = true; }    // event at the start of frame 2
    public void SetSafe()
    { isDeadly = false; }   // event at the start of frame 4

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = other.GetComponent<PlayerController>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (isDeadly && playerInside != null)
            {playerInside.Die();}

    }
}
