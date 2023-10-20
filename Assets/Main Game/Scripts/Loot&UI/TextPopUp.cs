using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    private float disappearTimer;
    private TextMeshPro text;
    private Color textColor;
    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        disappearTimer = 1f;
        textColor = text.color;
    }

    private void Update()
    {
        float moveYSpeed = 0.85f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 2f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
