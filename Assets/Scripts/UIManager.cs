using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] List<Image> playerHealth = new List<Image>();

    [SerializeField] Canvas invadersCanvas;
    [SerializeField] Canvas storyCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void DisplayHealthIcons(int number)
    {
        if (number <= 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            for (int i = playerHealth.Count; i > number; i--)
            {
                playerHealth[i - 1].color = Color.clear;
            }
        }
    }

    public void UpdateCanvases()
    {
    switch (GameManager.Instance.gameState)
        {
            case GameManager.GameState.MainMenu:
                invadersCanvas.enabled = false;
                storyCanvas.enabled = false;
                break;
            case GameManager.GameState.IntroText:
                invadersCanvas.enabled = false;
                storyCanvas.enabled = true;
                break;
            case GameManager.GameState.Invaders:
                invadersCanvas.enabled = true;
                storyCanvas.enabled = false;
                break;
            case GameManager.GameState.GameOver:
                invadersCanvas.enabled = false;
                storyCanvas.enabled = false;
                break;
            default:
                break;
        }
    }
}
