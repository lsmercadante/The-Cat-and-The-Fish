using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0f, 1f)] public float parallaxFactorX = 0.3f;
    [Range(0f, 1f)] public float parallaxFactorY = 0f; //this is kept at zero to keep things from drifting on jumps
    public Transform cameraTransform;       // a transform componenent of a camera
    private Vector3 startPosition;        // this layer's position when the level starts
    private Vector3 cameraStartPosition;  // the camera's position when the level starts
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        { cameraTransform = Camera.main.transform; }    // sets the camera transform to be that of the main camera

        startPosition = transform.position;         // this is the position of the game object this script attaches to

        if (cameraTransform != null)
            {cameraStartPosition = cameraTransform.position; }    // this sets the camera position

    }

     private void LateUpdate()          // LateuUpdate because that is where the camera cinemabrain updates
    {
        if (cameraTransform == null)
            {return;}
 
        // How far the camera has traveled from where it began.
        Vector3 travel = cameraTransform.position - cameraStartPosition;
 
        // Offset this layer by a fraction of that travel. Absolute (not additive)
        // so it can never accumulate drift over a long level.
        transform.position = new Vector3(
            startPosition.x + travel.x * parallaxFactorX,
            startPosition.y + travel.y * parallaxFactorY,
            startPosition.z);
    }
}
