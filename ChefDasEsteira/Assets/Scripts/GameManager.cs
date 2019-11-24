using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxErrorsAllowed;
    private int errorCount;


    public void IncreaseErrorCount(int errorIncrement)
    {
        errorCount += errorIncrement;
        if(errorCount >= maxErrorsAllowed)
        {
            EndGame();
        }
    }

    public void DecreaseErrorCount(int errorDecrement)
    {
        errorCount -= errorDecrement;
        if (errorCount <= 0)
            errorCount = 0;
    }

    public void EndGame()
    {
        // Load results screen
        Debug.Log("Game over!");
    }

}
