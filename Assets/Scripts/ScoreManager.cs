using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float gameSpeed;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score;
    [SerializeField] List<Image> playerHealth = new List<Image>();

    float lastGameSpeed;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Time.timeScale = gameSpeed;
        UpdateScoreText();
    }

    private void Update()
    {
        GameSpeedUpdate();
    }

    private void GameSpeedUpdate()
    {
        if(gameSpeed != lastGameSpeed)
        {
            Debug.Log("Game Speed set to: " + gameSpeed.ToString());
            Time.timeScale = gameSpeed;
            lastGameSpeed = gameSpeed;
        }
    }

    public void ChangeScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void DisplayHealthIcons(int number)
    {
        if(number <= 0)
        {
            GameOver();
        }
        else
        {
            for (int i = playerHealth.Count - 1; i > number; i--)
            {
                playerHealth[i].color = Color.clear;
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over.");
    }

}
