using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int scoreMultiplier;

    private int score = 0;
    public static ScoreCounter instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InvokeRepeating("addScoreIndefinitely", 0f, .5f);
    }

    public void stopScoreAddition()
    {
        CancelInvoke("addScoreIndefinitely");
    }

    private void addScoreIndefinitely()
    {
        score += scoreMultiplier;
        UIController.instance.setScoreText(score);
    }

    public void addScore(int addition)
    {
        score += addition;
        UIController.instance.setScoreText(score);
    }

    public int getScore()
    {
        return score;
    }
}
