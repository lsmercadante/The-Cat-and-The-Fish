using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class WinScreenController : MonoBehaviour
{
    [SerializeField] int sardinesPerLevel = 5;
    Button restartButton;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var messageText = root.Q<Label>("MessageText");
        var deathCountText = root.Q<Label>("DeathCountText");
        var sardineCountText = root.Q<Label>("SardineCountText");
        var timeText = root.Q<Label>("TimeText");
        restartButton = root.Q<Button>("RestartButton");

        messageText.text = "You got all the golden fish!";

        if (GameManager.Instance != null)
        {
            deathCountText.text =
                $"Deaths: {GameManager.Instance.totalDeaths}\n" +
                $"Garden: {GameManager.Instance.level1Deaths}   " +
                $"Rooftop: {GameManager.Instance.level2Deaths}";

            float t1 = GameManager.Instance.level1Time;
            float t2 = GameManager.Instance.level2Time;
            timeText.text =
                $"Time: {GameManager.FormatTime(t1 + t2)}\n" +
                $"Garden: {GameManager.FormatTime(t1)}   " +
                $"Rooftop: {GameManager.FormatTime(t2)}";

            int s1 = GameManager.Instance.sardinesLevel1;
            int s2 = GameManager.Instance.sardinesLevel2;
            int total = s1 + s2;
            int max = sardinesPerLevel * 2;

            sardineCountText.text =
                $"Sardines: {total}/{max}\n" +
                $"Garden: {s1}/{sardinesPerLevel}   " +
                $"Rooftop: {s2}/{sardinesPerLevel}";
            if (total == max)
                sardineCountText.text += "\nEvery last one!";
        }
        else
        {
            deathCountText.text = "Deaths: 0";
            sardineCountText.text = $"Sardines: 0/{sardinesPerLevel * 2}";
        }

        restartButton.clicked += OnRestartPressed;
    }
    void OnDisable()
    {
        if (restartButton != null)
            restartButton.clicked -= OnRestartPressed;
    }

    void OnRestartPressed()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ResetRun();

        SceneManager.LoadScene("MainMenu");
    }

}
