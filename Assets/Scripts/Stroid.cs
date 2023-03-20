using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stroid : MonoBehaviour
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float minSpin;
    [SerializeField] float maxSpin;

    [SerializeField] int health;

    Vector3 direction;
    float spin;

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        direction = new Vector3(x, y, 0);
        direction = direction.normalized * Random.Range(minSpeed, maxSpeed);

        spin = Random.Range(minSpeed, maxSpin) * (Random.value < .5f ? 1 : -1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * Time.timeScale, Space.World);
        transform.Rotate(0, 0, -spin * Time.deltaTime * Time.timeScale);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            GameManager.Instance.ScoreManager.PlayerBulletsHit += 1;
            health -= 1;
            if (health <= 0) { Death(); }
        }
    }

    private void Death()
    {
        //spawn resource
        Destroy(gameObject);
    }
}
