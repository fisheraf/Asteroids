using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    [SerializeField] int initialHealth;
    [SerializeField] int health;
    [SerializeField] List<GameObject> blocks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = blocks.Count;
        health = initialHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetHealth()
    {
        return health;
    }

    public void RemoveBlock(GameObject block)
    {
        blocks.Remove(block);
        health = blocks.Count;
    }
}
