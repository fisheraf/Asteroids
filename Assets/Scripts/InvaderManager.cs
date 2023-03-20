using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderManager : MonoBehaviour
{
    [SerializeField] GameObject InvadersGame;
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

    [SerializeField] GameObject bunkerPrefab;

    [SerializeField] BoxCollider2D[] bulletKillers;

    InvaderMovement invaderMovement;

    Planet planet;

    // Start is called before the first frame update
    void Start()
    {
        invaderMovement = FindObjectOfType<InvaderMovement>();

        NewRandomReloadTime();
        NewRandomSpawnTime();
    }

    public void StartInvaderGame(Planet _planet, int rowCount, int columnCount, int invaderScoreValue)
    {
        SetInvadersGameLocation(GameManager.Instance.Player.transform.position);
        GameManager.Instance.SetGameState(GameManager.GameState.Invaders);
        planet = _planet;
        SetInvaderSpeed(startingInvaderSpeed);
        NewRandomReloadTime();
        NewRandomSpawnTime();

        CreateNewBunkers();

        CreateInvaderWave(rowCount, columnCount, invaderScoreValue);

        GameManager.Instance.Player.transform.SetPositionAndRotation(new Vector3(InvadersGame.transform.position.x, InvadersGame.transform.position.y -9f, -1), Quaternion.identity);
    }

    public void EndInvaderGame()
    {
        planet.InvadersPresent = false;
        planet = null;
        DestroyBunkers();
        DestroyMysteryShip();
        SetBulletKillersActive(false);
        GameManager.Instance.SetGameState(GameManager.GameState.OverWorld); //World menu? text?
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Invaders)
        {
            InvadersShoot();

            MysteryShipSpawnTimer();
        }
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
            Debug.Log("Round " + GameManager.Instance.ScoreManager.Round.ToString() + " over.");
            GameManager.Instance.ScoreManager.Round += 1;
            //scoreManager.GameSpeed *= 1.1f;
            invaderMovement.MovementSpeed *= 1.2f;
            /*switch (scoreManager.Round)
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
            }*/

            EndInvaderGame();
        }
    }

    public void SetInvaderSpeed(float speed)
    {
        invaderMovement.MovementSpeed = speed;  
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
    private void DestroyMysteryShip()
    {
        GameObject mysteryShip = GameObject.Find("Mystery Ship");
        if (mysteryShip != null)
        {
            Destroy(mysteryShip);
        }
    }

    public void CreateInvaderWave(int rowCount, int columnCount, int invaderScoreValue)
    {
        invaderHolder.transform.localPosition = new Vector3(0, 8);

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                GameObject invaderObject = Instantiate(invaderPrefab, invaderHolder.transform, false);
                invaderObject.transform.localPosition = new Vector3(-19f + 1.5f * j, -1.5f * i, 0);
                Invader invader = invaderObject.GetComponent<Invader>();
                invader.SetScoreValue(invaderScoreValue);//dynamic by row?
                ListOfInvaders.Add(invader);
            }
        }
    }
    
    private void CreateNewBunkers()
    {
        GameObject bunkerHolder = new GameObject("Bunker Holder");
        bunkerHolder.transform.SetParent(InvadersGame.transform);
        bunkerHolder.transform.localPosition = Vector3.zero;

        for (int i = 0; i < 5; i++)
        {
            GameObject bunker = Instantiate(bunkerPrefab, bunkerHolder.transform, false);
            bunker.transform.localPosition = new Vector3(-14 + 7f * i, -6.5f, 0);
            bunker.name = "Bunker " + i;
            //Instantiate(bunkerPrefab, new Vector3(-14 + 7f * i, -6.5f, 0), Quaternion.identity);
        }
    }

    private void DestroyBunkers()
    {
        Destroy(GameObject.Find("Bunker Holder"));
    }


    public void SetBulletKillersActive(bool b)
    {
        foreach (BoxCollider2D boxCollider2D in bulletKillers)
        {
            boxCollider2D.enabled = b;
        }
    }

    public void SetInvadersGameLocation(Vector2 location)
    {
        InvadersGame.transform.position = new Vector3(location.x, location.y, -1);
    }
}
