using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text; 

    // Start is called before the first frame update
    void Start()
    {
        
    }
    float frameCount = 0;
    float dt = 0.0f;
    float fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per sec.

    // Update is called once per frame
    void Update()
    {      
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0 / updateRate)
        {
            fps = frameCount / dt;
            text.text = fps.ToString();
            frameCount = 0;
            dt -= 1.0f / updateRate;
        }
    }
}
