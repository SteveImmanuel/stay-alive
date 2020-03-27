using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text scoreText;
    public int scoreMultiplier;

    private int score = 2;
    private float elapsedTime;
    
    public void addScore(int addition)
    {
        score += addition;
        scoreText.text = "Score: " + score;
    }

    public void setScore(int newScore)
    {
        score = newScore;
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        setScore(Mathf.RoundToInt(elapsedTime * scoreMultiplier));
    }
}
