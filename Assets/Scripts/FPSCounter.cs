using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    TextMeshProUGUI fpsCounter;


    // Start is called before the first frame update
    void Start()
    {
        fpsCounter = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter.text = Mathf.RoundToInt(1f / Time.smoothDeltaTime).ToString();
    }
}
