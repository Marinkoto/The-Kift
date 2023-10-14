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
        }
    }
    public void SetPosition()
    {
        PlayerStats.instance.Invoke("SetStartPosition", 1f);
        
    }
    public IEnumerator StopTime()
    {
        yield return new WaitForSeconds(1.2f);
        Time.timeScale = 0f;
    }
}
