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

    public float reloadTimer;

    [Header("Movement")]
    [SerializeField] float movementSpeed;

    [Header("Health")]
    [SerializeField] int health;

    ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();                

        void Shooting()
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && (reloadTimer >= reloadTime))
            {
                reloadTimer = 0f;
                Shoot();
            }

            reloadTimer += Time.deltaTime;
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
        Instantiate(bulletPrefab, fireLocation.position, fireLocation.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage();
    }

    private void TakeDamage()
    {
        health -= 1;
        scoreManager.DisplayHealthIcons(health);
    }
}
