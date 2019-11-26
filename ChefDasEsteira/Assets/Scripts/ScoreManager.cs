using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Score score;
    private int currentScore;

    private void Start()
    {
        scoreText.text = "0";
        score.totalScore = 0;
    }

    public void IncreaseScore(int scoreIncrement)
    {
        currentScore += scoreIncrement;
        score.totalScore = currentScore;
        scoreText.text = currentScore.ToString();
    }
    public void DecreaseScore(int scoreDecrement)
    {
        currentScore -= scoreDecrement;
        score.totalScore = currentScore;
        scoreText.text = currentScore.ToString();
    }

    public int GetScore()
    {
        return currentScore; 
    }
}
