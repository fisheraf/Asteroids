using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderManager : MonoBehaviour
{
    [SerializeField] float startingInvaderSpeed;
    [SerializeField] List<Invader> ListOfInvaders = new List<Invader>();

    [SerializeField] GameObject invaderHolder;

    [SerializeField] float reloadTime;
    public float reloadTimer;


    // Start is called before the first frame update
    void Start()
    {
        SetInvaderSpeed(startingInvaderSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        InvadersShoot();
    }

    private void InvadersShoot()
    {
        if(ListOfInvaders.Count == 0)
        {
            return;
        }
        if (reloadTimer >= reloadTime)
        {
            reloadTimer = 0f;
            int r = Random.Range(0, ListOfInvaders.Count);
            ListOfInvaders[r].Shoot();
        }

        reloadTimer += Time.deltaTime;
    }


    public void RemoveInvader(Invader invader)
    {
        ListOfInvaders.Remove(invader);
    }

    public void SetInvaderSpeed(float speed)
    {
        invaderHolder.GetComponent<InvaderMovement>().SetMovementSpeed(speed);        
    }
}
