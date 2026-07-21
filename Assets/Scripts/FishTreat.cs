using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FishTreat : MonoBehaviour
{
    
    [SerializeField] private WinPanelUI winPanel;
    [SerializeField] private string nextSceneName = "Level2";
    [SerializeField] private float delayBeforeLoad = 1.5f;
    private bool collected = false;   // guard so it only ever fires once
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;   // only the cat triggers the goal

        collected = true;
        StartCoroutine(CollectRoutine());
    }
    private IEnumerator CollectRoutine()
    {

        if (winPanel != null)
        { winPanel.Show(); }
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene(nextSceneName);     // on to the next level
    }
}