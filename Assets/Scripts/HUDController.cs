using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HUDController : MonoBehaviour
{
    Label deathCountLabel;
    int lastDisplayedDeaths = -1;
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        deathCountLabel = root.Q<Label>("DeathCountLabel");
        UpdateDeathCount();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateDeathCount();

    }

    void UpdateDeathCount()
    {
        if (deathCountLabel == null) return;

        int deaths = GameManager.Instance != null ? GameManager.Instance.totalDeaths : 0;

        if (deaths != lastDisplayedDeaths)
        {
            deathCountLabel.text = deaths.ToString();
            lastDisplayedDeaths = deaths;
        }
}
}