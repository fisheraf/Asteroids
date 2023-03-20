using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenIndicator : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] objects;

    [SerializeField] SpriteRenderer[] indicators;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < objects.Length; i++)        
        {
            GameObject o = objects[i];
            Vector2 direction = player.transform.position - o.transform.position;
            RaycastHit2D ray = Physics2D.Raycast(o.transform.position, direction, 1000, ~LayerMask.NameToLayer("OffScreenIndicator"));            
            Debug.DrawRay(o.transform.position, direction);

            if (ray.collider.name == "OffScreenCollider")
            {
                Debug.Log(ray.transform.name);
                indicators[i].enabled = true;
                indicators[i].transform.position = ray.point;
            }
            else
            {
                indicators[i].enabled = false;
            }
        }
    }
}