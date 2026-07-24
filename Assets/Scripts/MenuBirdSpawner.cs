using UnityEngine;

public class MenuBirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float minInterval = 3f;
    public float maxInterval = 8f;
    public Vector2 spawnPosition = new Vector2(-15f, 4f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(SpawnBird), Random.Range(minInterval, maxInterval));
    }

    void SpawnBird()
    {
        Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
        Invoke(nameof(SpawnBird), Random.Range(minInterval, maxInterval));
    }
}
