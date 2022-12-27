using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderManager : MonoBehaviour
{
    [SerializeField] GameObject invaderPrefab;
    [SerializeField] float startingInvaderSpeed;
    [SerializeField] List<Invader> ListOfInvaders = new List<Invader>();

    [SerializeField] GameObject invaderHolder;

    [SerializeField] float minReloadTime;
    [SerializeField] float maxReloadTime;
    [SerializeField] float randomReloadTime;
    public float reloadTimer;

    [SerializeField] GameObject mysteryShipPrefab;
    [SerializeField] Transform mysteryShipSpawnLocation;
    [SerializeField] float minSpawn, maxSpawn;
    public float randomSpawnTime;
    public float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        SetInvaderSpeed(startingInvaderSpeed);
        NewRandomReloadTime();
        NewRandomSpawnTime();

        CreateInvaderWave();
    }

    // Update is called once per frame
    void Update()
    {
        InvadersShoot();

        MysteryShipSpawnTimer();
    }

    private void InvadersShoot()
    {
        if(ListOfInvaders.Count == 0)
        {
            return;
        }
        if (reloadTimer >= randomReloadTime)
        {
            reloadTimer = 0f;
            int r = Random.Range(0, ListOfInvaders.Count);
            ListOfInvaders[r].Shoot();
            NewRandomReloadTime();
        }

        reloadTimer += Time.deltaTime;
    }

    private void NewRandomReloadTime()
    {
        randomReloadTime = Random.Range(minReloadTime, maxReloadTime);
    }


    public void RemoveInvader(Invader invader)
    {
        ListOfInvaders.Remove(invader);
    }

    public void SetInvaderSpeed(float speed)
    {
        invaderHolder.GetComponent<InvaderMovement>().SetMovementSpeed(speed);        
    }

    private void MysteryShipSpawnTimer()
    {
        if(spawnTimer >= randomSpawnTime)
        {
            spawnTimer = 0;
            SpawnMysteryShip();
            NewRandomSpawnTime();
        }

        spawnTimer += Time.deltaTime;
    }

    private void NewRandomSpawnTime()
    {
        randomSpawnTime = Random.Range(minSpawn, maxSpawn);
    }

    public void SpawnMysteryShip()
    {
        Instantiate(mysteryShipPrefab, mysteryShipSpawnLocation.position, mysteryShipSpawnLocation.rotation);
    }

    public void CreateInvaderWave()
    {
        for (int i = 0; i < 5; i++)  //turn 5 to dynamic
        {
            for (int j = 0; j < 11; j++)  //turn 11 to dynamic
            {
                GameObject invaderObject = Instantiate(invaderPrefab, new Vector3(-19 + 1.2f * j, 8 - 2 * i, 0), Quaternion.identity, invaderHolder.transform);
                ListOfInvaders.Add(invaderObject.GetComponent<Invader>());
            }
        }
    }
}
