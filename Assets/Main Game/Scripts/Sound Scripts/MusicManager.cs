using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip currentMusic;
    private void Start()
    {
        AudioManager.instance.MusicSource.clip = currentMusic;
        AudioManager.instance.MusicSource.Play();
    }
}
