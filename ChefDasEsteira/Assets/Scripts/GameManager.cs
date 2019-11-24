using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxErrorsAllowed;
    private int errorCount;
    private bool isFinishing = false;
    [SerializeField] private GameObject endGameFadeOut;


    public void IncreaseErrorCount(int errorIncrement)
    {
        errorCount += errorIncrement;
        if(!isFinishing && errorCount >= maxErrorsAllowed)
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
        endGameFadeOut.SetActive(true);
        Invoke("LoadGameOver", 3f);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
