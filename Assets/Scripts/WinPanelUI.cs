using UnityEngine;
using UnityEngine.UIElements;

public class WinPanelUI : MonoBehaviour
{
    private VisualElement panel;

    private void OnEnable()
    {
        // rootVisualElement is ready in OnEnable for a UIDocument on this object.
        var root = GetComponent<UIDocument>().rootVisualElement;
        panel = root.Q<VisualElement>("WinPanel");
        Debug.Log(panel == null ? "WinPanel element NOT found" : "WinPanel element found");
        Hide();   // start hidden
    }

    public void Show()
    {
        Debug.Log("Show() called");
        if (panel != null) panel.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        if (panel != null) panel.style.display = DisplayStyle.None;
    }
}
