using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    [SerializeField] float parallaxValue;

    Vector3 startingPostition;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPostition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 relativePosition = gameCamera.transform.position * parallaxValue;
        relativePosition.z = startingPostition.z;
        transform.position = startingPostition + relativePosition;
    }
}
