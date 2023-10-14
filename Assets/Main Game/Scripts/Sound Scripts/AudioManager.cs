using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Source")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource HitSFXSource;
    [SerializeField] AudioSource LevelUPSFXSource;
    [Header("Audio Clips")]
    public AudioClip menuMusic;
    public AudioClip playerDash;
    public AudioClip enemyHit;
    public AudioClip enterPortal;
    public AudioClip shootFish;
    public AudioClip shootLightning;
    public AudioClip shootLaser;
    public AudioClip playerHit;
    public AudioClip levelUp;
    public AudioClip axeSwoosh;
    public AudioClip itemDestroy;
    public AudioClip lootSound;

    private void Awake()
    {
        instance = this;
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
        musicSource.clip = menuMusic; 
        musicSource.Play();
    }
    public void PlaySFX(AudioClip _clip)
    {
        SFXSource.clip = _clip;
        SFXSource.Play();
    }
    public void PlayHitSFX(AudioClip _clip)
    {
        HitSFXSource.clip = _clip;
        HitSFXSource.Play();
    }
    public void PlayLevelUPSFX(AudioClip _clip)
    {
        LevelUPSFXSource.clip = _clip;
        LevelUPSFXSource.Play();
    }
}
