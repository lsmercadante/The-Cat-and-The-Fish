using UnityEngine;

public class SardineCollectable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectSardine();
            // audioSource.PlayOneShot(collectClip); // Week 4
            Destroy(gameObject);
        }
    }
}
