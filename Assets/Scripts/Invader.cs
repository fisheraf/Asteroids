using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader: MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] int direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementSpeed * direction * Time.deltaTime, 0, 0);

        if(transform.position.x > 19)
        {
            SetDirection(-1);
            MoveDownRow();
        }
        if (transform.position.x < -19)
        {
            SetDirection(1);
            MoveDownRow();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void SetDirection(int dir)
    {
        direction = dir;
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
}
