using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader: MonoBehaviour
{

    [SerializeField] int scoreValue;
    
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform fireLocation;

    private InvaderManager invaderManager;
    private InvaderMovement invaderMovement;

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);

        invaderManager = FindObjectOfType<InvaderManager>();
        invaderMovement = FindObjectOfType<InvaderMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 19.5 || transform.position.x < -19.5)
        {
            invaderMovement.ChangeDirectionAndMoveDownRow();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Death();
    }


    public void Shoot()
    {
        Instantiate(bulletPrefab, fireLocation.position, fireLocation.rotation);
    }

    void Death()
    {
        invaderManager.RemoveInvader(this);
        FindObjectOfType<ScoreManager>().ChangeScore(scoreValue);
        Destroy(gameObject);
    }
}
