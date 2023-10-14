using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip currentMusic;
    private void Start()
    {
        AudioManager.instance.musicSource.clip = currentMusic;
        AudioManager.instance.musicSource.Play();
    }
}
