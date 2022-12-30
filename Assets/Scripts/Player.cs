using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] float reloadTime;
    [SerializeField] float reloadTimer;

    [SerializeField] int maxBulletCount;
    [SerializeField] int bulletCount;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform fireLocation;
    

    [Header("Movement")]
    [SerializeField] float movementSpeed;

    [Header("Health")]
    [SerializeField] [Range(0,10)] int health;

    ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
        scoreManager = FindObjectOfType<ScoreManager>();

        scoreManager.DisplayHealthIcons(health);
    }

    // Update is called once per frame
    void Update()
    {
        //ShootingWithReloadTimer();
        ShootingWithBulletCount();
    }

    private void ShootingWithReloadTimer()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && (reloadTimer >= reloadTime))
        {
            reloadTimer = 0f;
            Shoot();
        }

        reloadTimer += Time.deltaTime;
    }

    private void ShootingWithBulletCount()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && (bulletCount < maxBulletCount))
        {
            Shoot();
        }
    }

    private void LateUpdate()
    {
        Movement();

        void Movement()
        {
            if (transform.position.x > -19 && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
            {
                transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
            }
            if (transform.position.x < 19 && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
            {
                transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, fireLocation.position, fireLocation.rotation);
        bullet.tag = "Player";
        AddBullet(1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage();
    }

    private void TakeDamage()
    {
        health -= 1;
        Debug.Log("Player hit. " + health.ToString() + " left.");
        scoreManager.DisplayHealthIcons(health);
    }

    private void AddBullet(int count)
    {
        bulletCount += count;
    }

    public void SubtractBullet(int count)
    {
        bulletCount -= count;
        if(bulletCount <= 0)
        {
            bulletCount = 0;
        }
    }
}
