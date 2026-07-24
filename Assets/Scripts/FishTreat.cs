using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FishTreat : MonoBehaviour
{

    [SerializeField] private WinPanelUI winPanel;
    [SerializeField] private string nextSceneName = "Level2";
    [SerializeField] private float delayBeforeLoad = 1.5f;
    private bool collected = false;   // guard so it only ever fires once
    public int levelIndex = 1;  // set to 2 on Level 2's goal object in the Inspector
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
        GameManager.Instance.timerRunning = false;
        yield return new WaitForSeconds(delayBeforeLoad);
        if (levelIndex == 1)
            {GameManager.Instance.sardinesLevel1 = GameManager.Instance.sardinesThisLevel;
            GameManager.Instance.level1Time = GameManager.Instance.levelTime;}
            else
            {GameManager.Instance.sardinesLevel2 = GameManager.Instance.sardinesThisLevel;
            GameManager.Instance.level2Time = GameManager.Instance.levelTime;}
        GameManager.Instance.sardinesThisLevel = 0;

        SceneManager.LoadScene(nextSceneName);     // on to the next level
    }
}