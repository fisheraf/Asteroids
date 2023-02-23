using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    [Serializable]
     public enum GameState
    {
        None,
        MainMenu,
        IntroText,
        Invaders,
        OverWorld,
        GameOver
    }

    public GameState gameState;
    public GameState lastGameState;

    [SerializeField] UIManager uiManager;
    [SerializeField] InvaderManager invaderManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Player player;
    [SerializeField] CameraFollow cameraFollow;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        SetGameState(GameState.MainMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
        lastGameState = gameState;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.IntroText)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SetGameState(GameState.Invaders);
            }
        }
    }

    private void OnValidate()
    {
        if(gameState != lastGameState)
        {
            SetGameState(gameState);
        }
    }

    public UIManager UIManager { get { return uiManager; } }
    public InvaderManager InvaderManager { get { return invaderManager; } }
    public ScoreManager ScoreManager { get { return scoreManager; } }
    public Player Player { get { return player; } }

    public void GameOver()
    {
        Debug.Log("Game Over.");
    }

    public void SetGameState(GameState state)
    {
        if(lastGameState == state) { return; }
        lastGameState = gameState;
        gameState = state;
        switch (state)
        {
            case GameState.MainMenu:
                Debug.Log("Gamestate set to main menu.");
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                break;
            case GameState.IntroText:
                Debug.Log("Gamestate set to intro text.");
                uiManager.SetButtonsActive(false);
                break;
            case GameState.Invaders:
                Debug.Log("Gamestate set to invaders.");
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                cameraFollow.SetCameraToStop();
                invaderManager.SetBulletKillersActive(true);
                //FindObjectOfType<ShootableButton>().SetInactive(); //attach in inspector?  switch statement for lastGameState?
                FindObjectOfType<Player>().cancelFirstShoot = true;
                invaderManager.StartInvaderGame();
                break;
            case GameState.OverWorld:
                Debug.Log("Gamestate set to OverWorld.");
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                cameraFollow.SetCameraToFollow();
                uiManager.SetButtonsActive(false);
                break;
            case GameState.GameOver:
                Debug.Log("Gamestate set to game over.");
                break;
            default:
                break;
        }

        uiManager.UpdateCanvases();
    }
}
