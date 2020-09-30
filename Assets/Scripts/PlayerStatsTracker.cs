using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class CurrentAndMaxNumber
{
    public int Number
    {
        get; private set;
    }
    private readonly int maxNumber;

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
    public int points = 0;

    private CurrentAndMaxNumber numberOfLives = new CurrentAndMaxNumber(4, 4);
    private CurrentAndMaxNumber numberOfStones = new CurrentAndMaxNumber(0, 5);
    private CurrentAndMaxNumber numberOfSticks = new CurrentAndMaxNumber(0, 10);

    private AudioSource playerAudio;

    [SerializeField] private AudioClip sellingSound;
    [SerializeField] private AudioClip nailHitSound;
    [SerializeField] private AudioClip powerUpSound;
    [SerializeField] private AudioClip pickingUpSound;



    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            numberOfLives.AddItem();
            playerAudio.PlayOneShot(powerUpSound);
            // Always remove PowerUP
            Destroy(other.gameObject.transform.parent.gameObject);
        } else if (other.gameObject.CompareTag("Nail"))
        {
            numberOfLives.RemoveItem();
            playerAudio.PlayOneShot(nailHitSound);

            //If we cannot remove more lives, then player is dead
            if (numberOfLives.CanRemoveItem() == false)
            {
                playerIsDead = true;
            } else // Remove nails which player hit
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        } else if (other.gameObject.CompareTag("Stick"))
        {
            playerAudio.PlayOneShot(pickingUpSound);
            if (numberOfSticks.CanAddItem())
            {
                numberOfSticks.AddItem();
                Destroy(other.gameObject.transform.parent.gameObject);
            } 
        } else if (other.gameObject.CompareTag("Stone"))
        {
            playerAudio.PlayOneShot(pickingUpSound);
            if (numberOfStones.CanAddItem())
            {
                numberOfStones.AddItem();
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Buyer"))
        {
            points += numberOfStones.Number * 5;
            points += numberOfSticks.Number * 2;
            numberOfStones.Reset();
            numberOfSticks.Reset();
            Debug.Log("Number of points " + points);
            playerAudio.PlayOneShot(sellingSound);
        }
    }
}
