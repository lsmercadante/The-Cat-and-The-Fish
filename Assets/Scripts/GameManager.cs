using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    public static GameManager Instance;

    public int totalDeaths = 0;
    public int level1Deaths = 0;
    public int level2Deaths = 0;

    public int sardinesThisLevel = 0;
    public int sardinesLevel1 = 0;
    public int sardinesLevel2 = 0;

    public float levelTime = 0f;      // running timer for the current level
    public float level1Time = 0f;
    public float level2Time = 0f;

    public bool timerRunning = false;

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
    void Update()
    {
        if (timerRunning)
            levelTime += Time.deltaTime;
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
        sardinesThisLevel = 0;
        sardinesLevel1 = 0;
        sardinesLevel2 = 0;
        level1Time = 0f;
        level2Time = 0f;
        levelTime = 0f;
        timerRunning = false;
    }
    public void CollectSardine()
    {
        sardinesThisLevel++;
    }
    public void StartLevelTimer()
    {
        levelTime = 0f;
        timerRunning = true;
    }
    public static string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        float seconds = t % 60f;
        return $"{minutes}:{seconds:00}";
    }
    public int CurrentLevelDeaths()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level2") return level2Deaths;
        return level1Deaths;
    }
}
