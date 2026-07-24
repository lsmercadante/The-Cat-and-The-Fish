using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HUDController : MonoBehaviour
{
    [SerializeField] int sardinesPerLevel = 5;
    Label deathCountLabel;
    Label sardineCountLabel;
    Label timerLabel;
    string lastDisplayedTime = "";
    int lastDisplayedDeaths = -1;
    int lastDisplayedSardines = -1;


    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        deathCountLabel = root.Q<Label>("DeathCountLabel");
        sardineCountLabel = root.Q<Label>("SardineCountLabel");
        timerLabel = root.Q<Label>("TimerLabel");
        Debug.Log("timerLabel found: " + (timerLabel != null));
        // Force a refresh on enable (scene loads reset the labels)
        lastDisplayedDeaths = -1;
        lastDisplayedSardines = -1;
        UpdateDeathCount();
        UpdateSardineCount();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateDeathCount();
        UpdateSardineCount();
        UpdateTimer();

    }

    void UpdateDeathCount()
    {
        if (deathCountLabel == null) return;

       int deaths = GameManager.Instance != null ? GameManager.Instance.CurrentLevelDeaths() : 0;

        if (deaths != lastDisplayedDeaths)
        {
            deathCountLabel.text = deaths.ToString();
            lastDisplayedDeaths = deaths;
        }
    }
    void UpdateSardineCount()
    {
        if (sardineCountLabel == null) return;

        int sardines = GameManager.Instance != null ? GameManager.Instance.sardinesThisLevel : 0;

        if (sardines != lastDisplayedSardines)
        {
            sardineCountLabel.text = sardines + "/" + sardinesPerLevel;
            lastDisplayedSardines = sardines;
        }
    }
    void UpdateTimer()
    {
        if (timerLabel == null) return;
        if (GameManager.Instance == null) return;

        string t = GameManager.FormatTime(GameManager.Instance.levelTime);
        if (t != lastDisplayedTime)
        {
            timerLabel.text = t;
            lastDisplayedTime = t;
        }
    }
}