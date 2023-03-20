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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("InvadersEdge"))
        {
            invaderMovement.ChangeDirectionAndMoveDownRow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            GameManager.Instance.ScoreManager.PlayerBulletsHit += 1;
            Death();
        }
    }


    public void Shoot()
    {
        Instantiate(bulletPrefab, fireLocation.position, fireLocation.rotation);
    }

    public void SetScoreValue(int value)
    {
        scoreValue = value;
    }

    void Death()
    {
        invaderManager.RemoveInvader(this);
        FindObjectOfType<ScoreManager>().ChangeScore(scoreValue);
        Destroy(gameObject);
    }
}
