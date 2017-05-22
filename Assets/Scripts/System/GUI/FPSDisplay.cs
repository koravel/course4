using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    float lastTime;
    public Text text;

    private void Start()
    {
        lastTime = Time.time;
    }

    void Update()
    {
        if(Time.time > lastTime + 1)
        {
            lastTime = Time.time;
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            text.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        }
    }
}