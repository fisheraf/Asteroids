using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustSpeed;
    [SerializeField] float rotationSpeed;

    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRigidbody.AddRelativeForce(Vector2.up * verticalInput * thrustSpeed);


        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, horizontalInput * -rotationSpeed * Time.deltaTime);
    }
}
