using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private float disappearTimer;
    private TextMeshPro text;
    private Color textColor;
    public Bullet bullet;
    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        disappearTimer = 1f;
        textColor = text.color;
    }

    private void Update()
    {
        float moveYSpeed = 3f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
