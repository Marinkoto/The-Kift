using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundEffect : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(PlaySFX);
    }
    void PlaySFX()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
    }
}
