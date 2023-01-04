using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float gameSpeed;
    [SerializeField] int round;

    [SerializeField] int score;

    [SerializeField] int playerBulletsFired;
    [SerializeField] int playerBulletsHit;

    float lastGameSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SetStartingScore();
    }

    private void SetStartingScore()
    {
        score = 0;
        //Time.timeScale = gameSpeed;
        GameManager.Instance.UIManager.UpdateScoreText(score);
    }

    private void Update()
    {
        //GameSpeedUpdate();
    }

    private void GameSpeedUpdate()
    {
        if (gameSpeed != lastGameSpeed)
        {
            Debug.Log("Game Speed set to: " + gameSpeed.ToString());
            Time.timeScale = gameSpeed;
            lastGameSpeed = gameSpeed;
        }
    }

    public void ChangeScore(int value)
    {
        score += value;
        GameManager.Instance.UIManager.UpdateScoreText(score);
    }

    public int Round
    {
        get { return round; }
        set { round = value; }
    }

    public float GameSpeed
    {
        get { return gameSpeed; }
        set { gameSpeed = value; }
    }

    public int PlayerBulletsFired
    {
        get { return playerBulletsFired; }
        set { playerBulletsFired = value; }
    }

    public int PlayerBulletsHit
    {
        get { return playerBulletsHit; }
        set { playerBulletsHit = value; }
    }
}
