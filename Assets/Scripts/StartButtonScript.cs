using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    private Button startButton;
    private GameObject titleScreen;
    private GameObject gameHudScreen;

    private PlayerStatsTracker playerStatsTrackerScript;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GetComponent<Button>();
        playerStatsTrackerScript = GameObject.Find("Player").GetComponent<PlayerStatsTracker>();
        titleScreen = GameObject.Find("TitleScreen");
        gameHudScreen = GameObject.Find("GameHUD");
        startButton.onClick.AddListener(StartGame);
        gameHudScreen.SetActive(false);
    }

    void StartGame()
    {
        titleScreen.SetActive(false);
        gameHudScreen.SetActive(true);
        playerStatsTrackerScript.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
