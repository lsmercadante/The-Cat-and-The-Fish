using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    bool activated;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;                       
        if (other.CompareTag("Player"))              
        {
            other.GetComponent<PlayerController>().SetRespawn(transform.position);
            activated = true;
            // optional: raise a flag / change color / play a chime here
        }
    }
}
