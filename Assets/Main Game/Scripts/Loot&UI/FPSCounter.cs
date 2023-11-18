using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;
    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }
    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
        fpsText.text = "FPS: " + Mathf.RoundToInt(CalculateFPS()).ToString();
    }
    private float CalculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;
    }
}
