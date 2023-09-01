using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    private bool waiting;
    public void HitStopEffect(float duration){
        if (waiting){
           return;
        }
        Time.timeScale = 0.0f;
        StartCoroutine(WaitEffect(duration));
    }
    IEnumerator WaitEffect(float duration){
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
        
}
