using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonScript : MonoBehaviour
{
    private Button restartButton;
    private PlayerStatsTracker playerStatsTrackerScript;

    // Start is called before the first frame update
    void Start()
    {
        restartButton = GetComponent<Button>();
        playerStatsTrackerScript = GameObject.Find("Player").GetComponent<PlayerStatsTracker>();
        restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        playerStatsTrackerScript.RestartGame();
    }
}
