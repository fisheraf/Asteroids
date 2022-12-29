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

    ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        SetInvaderSpeed(startingInvaderSpeed);
        NewRandomReloadTime();
        NewRandomSpawnTime();

        CreateInvaderWave(3,6,10);
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
        if(ListOfInvaders.Count == 0)
        {
            Debug.Log("Round " + scoreManager.Round.ToString() + " over.");
            scoreManager.Round += 1;
            scoreManager.GameSpeed *= 1.1f;
            switch (scoreManager.Round)
            {
                case 1:
                    CreateInvaderWave(3, 8, 15);
                    break;
                case 2:
                    CreateInvaderWave(4, 8, 20);
                    break;
                case 3:
                    CreateInvaderWave(5, 11, 25);
                    break;
            }
        }
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

    public void CreateInvaderWave(int rowCount, int columnCount, int invaderScoreValue)
    {
        for (int i = 0; i < rowCount; i++)  //turn 5 to dynamic
        {
            for (int j = 0; j < columnCount; j++)  //turn 11 to dynamic
            {
                GameObject invaderObject = Instantiate(invaderPrefab, new Vector3(-19 + 1.5f * j, 8 - 1.5f * i, 0), Quaternion.identity, invaderHolder.transform);
                Invader invader = invaderObject.GetComponent<Invader>();
                invader.SetScoreValue(invaderScoreValue);  //dynamic
                ListOfInvaders.Add(invader);
            }
        }
    }
}
