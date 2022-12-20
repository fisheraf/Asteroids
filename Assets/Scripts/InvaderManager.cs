using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderManager : MonoBehaviour
{
    [SerializeField] List<Invader> ListOfInvaders = new List<Invader>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
