using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    [SerializeField] int scoreValue;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Death();
    }

    void Death()
    {
        FindObjectOfType<ScoreManager>().ChangeScore(scoreValue);
        Destroy(gameObject);
    }
}
