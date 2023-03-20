using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] int rowCount;
    [SerializeField] int columnCount;
    [SerializeField] int invaderScoreValue;
    [SerializeField] private bool invadersPresent;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentsInChildren<SpriteRenderer>()[1].color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.Player.SetPlantInRange(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.Player.SetPlantInRange(null);
        }
    }

    public void InteractWithPlanet()
    {
        Debug.Log(gameObject.name + " interacted with.");
        if (InvadersPresent)
        {
            GameManager.Instance.InvaderManager.StartInvaderGame(this, rowCount, columnCount, invaderScoreValue);
        }
        else {Debug.Log(gameObject.name + " has been cleared of invaders.  Store TBD...."); }
    }

    public bool InvadersPresent { get { return invadersPresent; } set { invadersPresent = value; } }
}
