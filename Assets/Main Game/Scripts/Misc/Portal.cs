using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats.instance.Invoke("ActivateUI", 0.3f);
            Cursor.visible = true;
            AudioManager.instance.PlaySFX(AudioManager.instance.enterPortal);
            StartCoroutine(StopTime());
            PlayerStats.instance.currentClip = PlayerStats.instance.maxClipSize;
        }
    }
    public void SetPosition()
    {
        PlayerStats.instance.Invoke("StartPosition", 2f);
        
    }
    public IEnumerator StopTime()
    {
        yield return new WaitForSeconds(1.2f);
        Time.timeScale = 0f;
    }
}
