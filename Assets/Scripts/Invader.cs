using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader: MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] int direction;

    [SerializeField] int scoreValue;
    
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform fireLocation;

    private InvaderManager invaderManager;


    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);

        invaderManager = FindObjectOfType<InvaderManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementSpeed * direction * Time.deltaTime, 0, 0);

        if(transform.position.x > 19.5 || transform.position.x < -19.5)
        {
            invaderManager.ChangeDirectionAndMoveDownRow();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Death();
    }

    public void ChangeDirectionAndMoveDownRow()
    {
        ChangeDirection();
        MoveDownRow();
    }


    public void ChangeDirection()
    {
        direction *= -1;
    }

    public void MoveDownRow()
    {
        Vector3 position = transform.position;
        position.y -= 1;
        transform.position = position;
    }

    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
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
