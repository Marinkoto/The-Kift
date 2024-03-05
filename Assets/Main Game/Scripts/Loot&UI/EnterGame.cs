using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(DungeonManager.instance.dungeonCounter);
            AudioManager.instance.PlaySFX(AudioManager.instance.enterPortal);
        }
    }
}
