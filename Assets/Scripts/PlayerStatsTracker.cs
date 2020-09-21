using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class CurrentAndMaxNumber
{
    private int number;
    private int maxNumber;

    public CurrentAndMaxNumber(int startingQuantity, int maxNumber)
    {
        number = startingQuantity;
        this.maxNumber = maxNumber;
    }

    void AddItem()
    {
        if (number < maxNumber)
        {
            number++;
        }
    }

    void SetItemNumber(int newItemNumber)
    {
        number = newItemNumber;
    }

}

public class PlayerStatsTracker : MonoBehaviour
{

    private CurrentAndMaxNumber numberOfLives = new CurrentAndMaxNumber(4, 4);
    private CurrentAndMaxNumber numberOfStones = new CurrentAndMaxNumber(0, 5);
    private CurrentAndMaxNumber numberOfSticks = new CurrentAndMaxNumber(0, 10);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
