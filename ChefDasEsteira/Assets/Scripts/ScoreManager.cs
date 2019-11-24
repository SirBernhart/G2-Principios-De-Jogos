using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore;

    private void Start()
    {
        scoreText.text = "0";
    }

    public void IncreaseScore(int scoreIncrement)
    {
        currentScore += scoreIncrement;
        scoreText.text = currentScore.ToString();
    }
    public void DecreaseScore(int scoreDecrement)
    {
        currentScore -= scoreDecrement;
        scoreText.text = currentScore.ToString();
    }

    public int GetScore()
    {
        return currentScore; 
    }
}
