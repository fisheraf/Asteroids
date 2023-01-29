using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    [SerializeField] private bool cameraFollowing;

    float cameraDelay;


    // Start is called before the first frame update
    void Start()
    {
        cameraFollowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraFollowing)
        {
            float x = playerTransform.position.x;
            float y = playerTransform.position.y;
            transform.DOMove(new Vector3(x, y, -10), cameraDelay);
            //transform.position = new Vector3(x, y, -10);
        }
    }

    public void SetCameraToFollow()
    {
        cameraDelay = 1.5f;
        cameraFollowing = true;
        StartCoroutine(LowerCameraDelay());
    }

    IEnumerator LowerCameraDelay()
    {
        yield return new WaitForSeconds(1.6f);
        cameraDelay = 0;
    }

    public void SetCameraToStop()
    {
        cameraFollowing = false;
        transform.position = new Vector3(0, 0, -10);
    }
}
