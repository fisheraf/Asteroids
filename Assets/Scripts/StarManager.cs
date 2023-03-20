using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] GameObject planetPrefab;
    [SerializeField] Sprite[] planetSprites;
    [SerializeField] int starCount;
    [SerializeField] int planetCount;
    [SerializeField] int mapSize;

    [SerializeField] GameObject starLayer1;
    [SerializeField] GameObject starLayer2;
    [SerializeField] GameObject starLayer3;
    [SerializeField] GameObject planetLayer;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] Color color3;
    [SerializeField] Color color4;

    // Start is called before the first frame update
    void Start()
    {
        GenerateStars();
        GeneratePlanets();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3Int(-mapSize / 2, mapSize/ 2), new Vector3Int(mapSize / 2, mapSize/ 2));
        Gizmos.DrawLine(new Vector3Int(mapSize / 2, mapSize/ 2), new Vector3Int(mapSize / 2, -mapSize/ 2));
        Gizmos.DrawLine(new Vector3Int(mapSize / 2, -mapSize/ 2), new Vector3Int(-mapSize / 2, -mapSize/ 2));
        Gizmos.DrawLine(new Vector3Int(-mapSize / 2, -mapSize/ 2), new Vector3Int(-mapSize / 2, mapSize/ 2));

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3Int(-7500, 7500), new Vector3Int(7500, 7500));
        Gizmos.DrawLine(new Vector3Int(7500, 7500), new Vector3Int(7500, -7500));
        Gizmos.DrawLine(new Vector3Int(7500, -7500), new Vector3Int(-7500, -7500));
        Gizmos.DrawLine(new Vector3Int(-7500, -7500), new Vector3Int(-7500, 7500));
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void GenerateStars()
    {
        for (int i = 0; i < starCount; i++)
        {
            GameObject star = Instantiate(starPrefab, RandomLocation(), Quaternion.identity, starLayer1.transform);
            float r = Random.Range(.1f, .4f);
            star.transform.localScale = new Vector3(r, r);
            star.isStatic = true;
            star.layer = LayerMask.NameToLayer("Background");


            GameObject star2 =  Instantiate(starPrefab, RandomLocation(), Quaternion.identity, starLayer2.transform);
            float r2 = Random.Range(.1f, .4f);
            star2.transform.localScale = new Vector3(r2, r2);
            star2.GetComponent<SpriteRenderer>().color = color1;
            if(i % 3 == 0)
            {
                star2.GetComponent<SpriteRenderer>().color = color2;
            }
            star2.isStatic = true;
            star2.layer = LayerMask.NameToLayer("Background");

            GameObject star3 = Instantiate(starPrefab, RandomLocation(), Quaternion.identity, starLayer3.transform);
            float r3 = Random.Range(.1f, .3f);
            star3.transform.localScale = new Vector3(r3, r3);
            star3.GetComponent<SpriteRenderer>().color = color3;
            if (i % 3 == 0)
            {
                star3.GetComponent<SpriteRenderer>().color = color4;
            }
            star3.isStatic = true;
            star3.layer = LayerMask.NameToLayer("Background");
        }
    }

    private void GeneratePlanets()
    {
        for (int i = 0; i < planetCount; i++)
        {
            GameObject planet = Instantiate(planetPrefab, RandomLocation(), Quaternion.identity, planetLayer.transform);
            float r = Random.Range(4f, 10f);
            planet.transform.localScale = new Vector3(r, r);
            planet.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 4) * 90f);

            SpriteRenderer sr = planet.GetComponent<SpriteRenderer>();
            sr.sprite = planetSprites[Random.Range(0, planetSprites.Length)];
            sr.color = Random.ColorHSV(0, 1, .5f, 1, .07f, .35f);
            planet.isStatic = true;
        }
    }

    Vector3 RandomLocation()
    {
        Vector3 v = new Vector3(Random.Range(-mapSize/2f, mapSize/2f), Random.Range(-mapSize/2f, mapSize/2f));

        return v;
    }
}
