using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    public static GameManager Instance;

    public int totalDeaths = 0;
    public int level1Deaths = 0;
    public int level2Deaths = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void RegisterDeath()
    {
        totalDeaths++;
        Debug.Log($"Death registered. Total: {totalDeaths}");
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level1") level1Deaths++;
        else if (sceneName == "Level2") level2Deaths++;
    }
    public void ResetRun()
    {
        totalDeaths = 0;
        level1Deaths = 0;
        level2Deaths = 0;
    }
}
