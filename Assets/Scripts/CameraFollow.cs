using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private bool minimapCamera;
    [SerializeField] Transform playerTransform;

    [SerializeField] private bool cameraFollowing;

    [SerializeField] float cameraDelay;

    float tweenTime;


    // Start is called before the first frame update
    void Start()
    {
        if (minimapCamera)
        {
            cameraFollowing = true;
        }
        else
        {
            cameraFollowing = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraFollowing)
        {
            if(tweenTime > 0)
            {
                tweenTime -= Time.deltaTime;
            }
            else { tweenTime = 0; }            

            float x = playerTransform.position.x;
            float y = playerTransform.position.y;
            transform.DOMove(new Vector3(x, y, -10), tweenTime).SetEase(Ease.InOutCubic);
            //transform.position = new Vector3(x, y, -10);
        }
    }

    public void SetCameraToFollow()
    {
        cameraDelay = 1f;
        tweenTime = cameraDelay;
        cameraFollowing = true;
        StartCoroutine(LowerCameraDelay());
    }

    IEnumerator LowerCameraDelay()
    {
        yield return new WaitForSeconds(cameraDelay +.1f);
        cameraDelay = 0;
    }

    public void SetCameraToStop()
    {
        cameraFollowing = false;
        //transform.position = new Vector3(0, 0, -10);
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
    }
}
