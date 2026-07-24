using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenuController : MonoBehaviour
{
    Button playButton;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        playButton = root.Q<Button>("PlayButton");
        playButton.clicked += OnPlayPressed;
    }

    void OnDisable()
    {
        if (playButton != null)
        { playButton.clicked -= OnPlayPressed; }
    }
    void OnPlayPressed()
    {
        SceneManager.LoadScene("Level1");
    }
}
