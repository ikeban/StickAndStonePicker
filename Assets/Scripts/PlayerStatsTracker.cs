using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

class CurrentAndMaxNumber
{
    public int Number
    {
        get; private set;
    }
    public readonly int maxNumber;

    public CurrentAndMaxNumber(int startingQuantity, int maxNumber)
    {
        Number = startingQuantity;
        this.maxNumber = maxNumber;
    }

    public bool CanAddItem()
    {
        return (Number < maxNumber);
    }

    public bool CanRemoveItem()
    {
        return (Number > 0);
    }

    public void AddItem()
    {
        if (CanAddItem())
        {
            Number++;
            Log();
        }
    }

    public void RemoveItem()
    {
        if (CanRemoveItem())
        {
            Number--;
            Log();
        }
    }

    public void Reset()
    {
        Number = 0;
    }

    public void Log()
    {
        Debug.Log("Current Value is " + Number);
    }

}

public class PlayerStatsTracker : MonoBehaviour
{
    public bool playerIsDead = false;
    public bool gameIsActive = false;
    public int points = 0;

    private CurrentAndMaxNumber numberOfLives = new CurrentAndMaxNumber(4, 4);
    private CurrentAndMaxNumber numberOfStones = new CurrentAndMaxNumber(0, 5);
    private CurrentAndMaxNumber numberOfSticks = new CurrentAndMaxNumber(0, 10);

    private AudioSource playerAudio;
    private GameObject gameOverScreen;
    private int timeLeft = 120;

    [SerializeField] private AudioClip sellingSound = null;
    [SerializeField] private AudioClip nailHitSound = null;
    [SerializeField] private AudioClip powerUpSound = null;
    [SerializeField] private AudioClip pickingUpSound = null;

    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI timeText = null;
    [SerializeField] private TextMeshProUGUI livesText = null;
    [SerializeField] private TextMeshProUGUI stickText = null;
    [SerializeField] private TextMeshProUGUI stoneText = null;


    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        gameOverScreen = GameObject.Find("GameOverScreen");
        gameOverScreen.SetActive(false);
        playerIsDead = false;
        gameIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        playerIsDead = false;
        gameIsActive = true;
        gameOverScreen.SetActive(false);
        timeLeft = 120;
        UpdateHudDisplay();
        StartCoroutine(TimerDown());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameOver()
    {
        playerIsDead = true;
        gameIsActive = false;
        gameOverScreen.SetActive(true);
    }

    private void UpdateHudDisplay()
    {
        scoreText.text = "Score: " + points;
        livesText.text = "Lives: " + numberOfLives.Number + "/" + numberOfLives.maxNumber;
        stickText.text = "Sticks: " + numberOfSticks.Number + "/" + numberOfSticks.maxNumber;
        stoneText.text = "Stones: " + numberOfStones.Number + "/" + numberOfStones.maxNumber;
    }

    IEnumerator TimerDown()
    {
        while (gameIsActive)
        {
            timeText.text = "Time: " + timeLeft;
            yield return new WaitForSeconds(1);
            timeLeft--;
            timeText.text = "Time: " + timeLeft;

            if (timeLeft == 0)
            {
                GameOver();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            numberOfLives.AddItem();
            playerAudio.PlayOneShot(powerUpSound);
            UpdateHudDisplay();
            // Always remove PowerUP
            Destroy(other.gameObject.transform.parent.gameObject);
        } else if (other.gameObject.CompareTag("Nail"))
        {
            numberOfLives.RemoveItem();
            playerAudio.PlayOneShot(nailHitSound);

            //If we cannot remove more lives, then player is dead
            if (numberOfLives.CanRemoveItem() == false)
            {
                GameOver();
            } else // Remove nails which player hit
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
            UpdateHudDisplay();
        } else if (other.gameObject.CompareTag("Stick"))
        {
            playerAudio.PlayOneShot(pickingUpSound);
            if (numberOfSticks.CanAddItem())
            {
                numberOfSticks.AddItem();
                Destroy(other.gameObject.transform.parent.gameObject);
            }
            UpdateHudDisplay();
        } else if (other.gameObject.CompareTag("Stone"))
        {
            playerAudio.PlayOneShot(pickingUpSound);
            if (numberOfStones.CanAddItem())
            {
                numberOfStones.AddItem();
                Destroy(other.gameObject.transform.parent.gameObject);
            }
            UpdateHudDisplay();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Buyer"))
        {
            int oldPointsValue = points;
            points += numberOfStones.Number * 5;
            points += numberOfSticks.Number * 2;
            numberOfStones.Reset();
            numberOfSticks.Reset();
            Debug.Log("Number of points " + points);
            // Pley selling sound if really player sold something
            if (points > oldPointsValue)
            {
                playerAudio.PlayOneShot(sellingSound);
            }
            UpdateHudDisplay();
        }
    }
}
