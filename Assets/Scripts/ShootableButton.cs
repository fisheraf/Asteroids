using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootableButton : MonoBehaviour
{
    [Serializable]
    public enum ButtonType
    {
        Start,
        OverWorld,
        Controls,
        Options,
        Exit
    }

    public ButtonType buttonType;
    [SerializeField] int hits;

    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = hits;
        slider.value = 0;

        switch (buttonType)
        {
            case ButtonType.Start:
                buttonText.text = "Start";
                break;
            case ButtonType.OverWorld:
                buttonText.text = "OverWorld";
                break;
            case ButtonType.Controls:
                buttonText.text = "Controls: A & D to move, space to shoot";
                break;
            case ButtonType.Options:
                break;
            case ButtonType.Exit:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        slider.value += 1;
        if (slider.value == hits)
        {
            ButtonAction();
        }
    }

    public void ButtonAction()
    {
        switch (buttonType)
        {
            case ButtonType.Start:
                GameManager.Instance.SetGameState(GameManager.GameState.IntroText);
                break;
            case ButtonType.OverWorld:
                GameManager.Instance.SetGameState(GameManager.GameState.OverWorld);
                break;
            case ButtonType.Controls:
                break;
            case ButtonType.Options:
                break;
            case ButtonType.Exit:
                break;
            default:
                break;
        }
        SetInactive();
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
