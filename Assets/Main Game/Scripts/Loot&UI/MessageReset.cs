using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageReset : MonoBehaviour
{
    public TextMeshProUGUI message;
    void Update()
    {
        if (message.text.Length >=1)
        {
            StartCoroutine(ResetMessage());
        }
    }
    public IEnumerator ResetMessage()
    {
        yield return new WaitForSeconds(5);
        this.message.text = "";
    }
}
