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
            PlayerStats.instance.Invoke("ActivateUI", 0.5f);
            Cursor.visible = true;
            AudioManager.instance.PlaySFX(AudioManager.instance.enterPortal);
        }
    }
    public void SetPosition()
    {
        PlayerStats.instance.Invoke("SetStartPosition", 0.275f);
    }
}
