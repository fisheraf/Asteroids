using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScoreText();
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

}
