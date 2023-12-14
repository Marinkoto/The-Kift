using System.Collections;
using TMPro;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool isPlayerClose;
    public TextMeshProUGUI interactText;
    private bool isPressed;
    private bool didPress;
    public StoryManager storyManager;
    public Dialogue dialogue;
    private void Start()
    {
        storyManager = GameObject.Find("Story Manager").GetComponent<StoryManager>();
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
            storyManager.StartDialogue(dialogue);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
        Cursor.visible = true;
        AudioManager.instance.PlaySFX(AudioManager.instance.enterPortal);
        PlayerStats.instance.currentClip = PlayerStats.instance.maxClipSize;
        isPressed = false;
        interactText.text = string.Empty;
        didPress = true;
    }
}
