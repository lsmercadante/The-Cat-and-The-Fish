using UnityEngine;
using System.Collections;
/// A smoke/vent hazard that is only lethal during part of its 4-frame animation.

public class SmokeHazard : MonoBehaviour
{
    private bool isDeadly = false;              // starts safe on frame 1
    private PlayerController playerInside = null;
    [SerializeField] private float waitBetweenPuffs = 1f;
    [SerializeField] private float puffDuration = 2f;
    [SerializeField] private string puffStateName = "Smoke";
    [SerializeField] private float startOffset = 0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PuffCycle());
    }

    //Called by Animation Events on the smoke clip
    public void SetDeadly()
    { isDeadly = true; }    // event at the start of frame 2
    public void SetSafe()
    { isDeadly = false; }   // event at the start of frame 4
    private IEnumerator PuffCycle()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(startOffset);
        while (true)
        {
            // OFF: hide the smoke and wait
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(waitBetweenPuffs);

            // PUFF: show it and play the clip once from the start
            spriteRenderer.enabled = true;
            animator.Play(puffStateName, 0, 0f);
            yield return new WaitForSeconds(puffDuration);
        }
    }

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
        if (isDeadly && spriteRenderer.enabled && playerInside != null)
        { playerInside.Die(); }

    }
}
