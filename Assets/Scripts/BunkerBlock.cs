using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponentInParent<Bunker>().RemoveBlock(gameObject);
        Destroy(gameObject);
    }*/

    public void DestroyThisBlock()
    {
        Destroy(gameObject);
    }
}
