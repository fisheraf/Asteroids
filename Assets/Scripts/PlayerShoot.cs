using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float reloadTime;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform fireLocation;

    public float reloadTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && (reloadTimer >= reloadTime))
        {
            reloadTimer = 0f;
            Shoot();
        }

        reloadTimer += Time.deltaTime;
    }

    private void Shoot()
    {        
        Instantiate(bulletPrefab, fireLocation.position, fireLocation.rotation);
    }
}
