using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] float reloadTime;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform fireLocation;

    [SerializeField] int maxBulletCount;
    [SerializeField] List<GameObject> bulletList = new List<GameObject>();

    public float reloadTimer;

    [Header("Movement")]
    [SerializeField] float horizontalMovementSpeed;
    [SerializeField] float forwardMovementSpeed;
    [SerializeField] float rotationSpeed;

    [Header("Health")]
    [SerializeField] int health;

    public bool cancelFirstShoot;  //Input bug - separate input mangager?
    Rigidbody2D playerRigidbody2D;
    [SerializeField] ParticleSystem engineParticleSystem;

    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        engineParticleSystem.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //ShootingReloadTimer();
        if (GameManager.Instance.gameState == GameManager.GameState.Invaders || GameManager.Instance.gameState == GameManager.GameState.MainMenu)
        {
            ShootingBulletCount();
        }
        if(GameManager.Instance.gameState == GameManager.GameState.OverWorld)
        {
            MovementForOverWorld();
        }
    }
    private void LateUpdate()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Invaders || GameManager.Instance.gameState == GameManager.GameState.MainMenu)
        {
            MovementForInvaders();
        }
    }

    private void ShootingReloadTimer()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && (reloadTimer >= reloadTime))
        {
            reloadTimer = 0f;
            Shoot();
        }

        reloadTimer += Time.deltaTime;
    }

    private void ShootingBulletCount()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if(cancelFirstShoot) { Debug.Log("Cancel true"); cancelFirstShoot = false; return; }
            if(bulletList.Count < maxBulletCount)
            {
                Shoot();
            }            
        }
    }

    private void MovementForInvaders()
    {
        if (transform.position.x > -19 && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {
            transform.Translate(-horizontalMovementSpeed * Time.deltaTime, 0, 0);
        }
        if (transform.position.x < 19 && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            transform.Translate(horizontalMovementSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void MovementForOverWorld()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            playerRigidbody2D.AddRelativeForce(Vector2.up * forwardMovementSpeed, ForceMode2D.Impulse);
            if (engineParticleSystem.isStopped) { engineParticleSystem.Play(); }
        }
        else
        {
            if (engineParticleSystem.isPlaying) { engineParticleSystem.Stop(); }
        }
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotation);
    }


    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, fireLocation.position, fireLocation.rotation);
        bulletList.Add(bullet);
        if(GameManager.Instance.gameState == GameManager.GameState.Invaders)
        {
            GameManager.Instance.ScoreManager.PlayerBulletsFired += 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage();
    }

    private void TakeDamage()
    {
        health -= 1;
        GameManager.Instance.UIManager.DisplayHealthIcons(health);
    }

    public void RemoveBullet(GameObject bullet)
    {
        bulletList.Remove(bullet);
    }
}
