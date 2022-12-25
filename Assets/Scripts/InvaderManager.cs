using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderManager : MonoBehaviour
{
    [SerializeField] float startingInvaderSpeed;
    [SerializeField] List<Invader> ListOfInvaders = new List<Invader>();

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
        if (reloadTimer >= reloadTime)
        {
            reloadTimer = 0f;
            int r = Random.Range(0, ListOfInvaders.Count);
            ListOfInvaders[r].Shoot();
        }

        reloadTimer += Time.deltaTime;
    }

    public void ChangeDirectionAndMoveDownRow()
    {
        foreach (Invader i in ListOfInvaders)
        {
            i.ChangeDirectionAndMoveDownRow();
        }
    }

    public void RemoveInvader(Invader invader)
    {
        ListOfInvaders.Remove(invader);
    }

    public void SetInvaderSpeed(float speed)
    {
        foreach (Invader i in ListOfInvaders)
        {
            i.SetMovementSpeed(speed);
        }
    }
}
