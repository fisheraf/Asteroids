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
        Options,
        Exis
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
            case ButtonType.Options:
                break;
            case ButtonType.Exis:
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
                GameManager.Instance.SetGameState(GameManager.GameState.Invaders);
                break;
            case ButtonType.Options:
                break;
            case ButtonType.Exis:
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
