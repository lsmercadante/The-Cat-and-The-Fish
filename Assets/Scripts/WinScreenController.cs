using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class WinScreenController : MonoBehaviour
{
    Button restartButton;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var messageText = root.Q<Label>("MessageText");
        var deathCountText = root.Q<Label>("DeathCountText");
        restartButton = root.Q<Button>("RestartButton");

        messageText.text = "You got all the golden fish!";

        if (GameManager.Instance != null)
        {
            deathCountText.text =
                $"Deaths: {GameManager.Instance.totalDeaths}\n" +
                $"Garden: {GameManager.Instance.level1Deaths}   " +
                $"Rooftop: {GameManager.Instance.level2Deaths}";
        }
        else
        {
            deathCountText.text = "Deaths: 0";
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
