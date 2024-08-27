using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxErrorsAllowed;
    private int errorCount;
    private bool isFinishing = false;
    [SerializeField] private GameObject endGameFadeOut;
    [SerializeField] private AudioSource music;


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
        StartCoroutine(FadeMusic());
        Invoke("LoadGameOver", 3.5f);
    }

    IEnumerator FadeMusic()
    {
        for(float i = 3 ; i >= 0 ; i -= Time.deltaTime)
        {
            music.volume = i/3;
            Debug.Log(music.volume);
            yield return null;
        }
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}