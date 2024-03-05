using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Portal : MonoBehaviour
{
    public bool isPlayerClose;
    public TextMeshProUGUI interactText;
    private bool isPressed;
    private bool didPress;
    public GameObject dialogueTrigger;
    private void Start()
    {
        interactText = GameObject.Find("Interact Text").GetComponent<TextMeshProUGUI>();
        isPlayerClose = false;
        isPressed = false;
        didPress = false;
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && isPlayerClose && !isPressed)
        {
            didPress = true;
            isPressed = false;
            EnterPortal();
            StartCoroutine(StopTime());
            interactText.text = "";
        }
        else if(didPress)
        {
            isPressed = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactText.text = "Press E to interact";
            isPlayerClose = true;
            dialogueTrigger.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueTrigger.SetActive(false);
            isPlayerClose = false;
            interactText.text = string.Empty;
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
    public void EnterPortal()
    {
        PlayerStats.instance.Invoke("ActivateUI", 0.3f);
        AudioManager.instance.PlaySFX(AudioManager.instance.enterPortal);
        PlayerStats.instance.currentClip = PlayerStats.instance.maxClipSize;
        isPressed = false;
        interactText.text = string.Empty;
        didPress = true;
    }
}
