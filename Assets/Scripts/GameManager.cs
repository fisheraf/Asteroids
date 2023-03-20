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
    [SerializeField] OffScreenIndicator offScreenIndicator;
    [SerializeField] GameObject minimap;

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

        Application.targetFrameRate = 60;
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
        if(gameState == state) { return; }
        lastGameState = gameState;
        gameState = state;
        switch (state)
        {
            case GameState.GameOver:
                Debug.Log("Gamestate set to game over.");
                break;

            case GameState.IntroText:
                Debug.Log("Gamestate set to intro text.");
                uiManager.SetButtonsActive(false);
                break;

            case GameState.Invaders:
                Debug.Log("Gamestate set to invaders.");
                player.SetPlayerToInvadersMovement();
                cameraFollow.SetCameraToStop();
                invaderManager.SetBulletKillersActive(true);
                //FindObjectOfType<ShootableButton>().SetInactive(); //attach in inspector?  switch statement for lastGameState?
                //FindObjectOfType<Player>().cancelFirstShoot = true;  //no longer needed when coming from open world?
                offScreenIndicator.gameObject.SetActive(false);
                minimap.SetActive(false);
                break;

            case GameState.MainMenu:
                Debug.Log("Gamestate set to main menu.");
                player.SetPlayerToInvadersMovement();
                minimap.SetActive(false);
                break;

            case GameState.OverWorld:
                Debug.Log("Gamestate set to OverWorld.");
                player.SetPlayerToOverworldMovement();
                cameraFollow.SetCameraToFollow();
                uiManager.SetButtonsActive(false);
                offScreenIndicator.gameObject.SetActive(true);
                minimap.SetActive(true);
                break;

            default:
                break;
        }

        uiManager.UpdateCanvases();
    }
}
