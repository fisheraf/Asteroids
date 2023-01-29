using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] int starCount;
    [SerializeField] int mapSize;

    [SerializeField] GameObject layer1;
    [SerializeField] GameObject layer2;
    [SerializeField] Color color1;
    [SerializeField] Color color2;

    // Start is called before the first frame update
    void Start()
    {
        GenerateStars();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateStars()
    {
        for (int i = 0; i < starCount; i++)
        {
            GameObject star = Instantiate(starPrefab, RandomLocation(), Quaternion.identity, layer1.transform);
            float r = Random.Range(.1f, .5f);
            star.transform.localScale = new Vector3(r, r);


            GameObject star2 =  Instantiate(starPrefab, RandomLocation(), Quaternion.identity, layer2.transform);
            float r2 = Random.Range(.1f, .3f);
            star2.transform.localScale = new Vector3(r2, r2);
            star2.GetComponent<SpriteRenderer>().color = color1;
            if(i%3 == 0)
            {
                star2.GetComponent<SpriteRenderer>().color = color2;
            }
        }
    }

    Vector3 RandomLocation()
    {
        Vector3 v = new Vector3(Random.Range(-mapSize/2f, mapSize/2f), Random.Range(-mapSize/2f, mapSize/2f));

        return v;
    }
}
