using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] int direction;

    private InvaderManager invaderManager;

    private float changeDirectionTimer;

    // Start is called before the first frame update
    void Start()
    {
        invaderManager = FindObjectOfType<InvaderManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementSpeed * direction * Time.deltaTime, 0, 0);

        changeDirectionTimer -= Time.deltaTime;
    }

    public void ChangeDirectionAndMoveDownRow()
    {
        if (changeDirectionTimer < 0)
        {
            //Debug.Log("Direction Changed.");
            ChangeDirection();
            MoveDownRow();
            changeDirectionTimer = .2f;
        }
        else
        {
            //Debug.Log("Too soon to change direction");
        }
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
}
