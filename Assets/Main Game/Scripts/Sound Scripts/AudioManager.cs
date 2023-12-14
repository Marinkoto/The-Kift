using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public const bool isSet = false;
    public static AudioManager instance;
    [Header("Audio Source")]
    [SerializeField] public AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource HitSFXSource;
    [SerializeField] AudioSource LevelUPSFXSource;
    [SerializeField] AudioSource ShootFXSource;
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
    public AudioClip buttonClick;
    public AudioClip lootSound;
    public AudioClip healthPotion;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void Start()
    {
        if (!isSet)
        {
            DontDestroyOnLoad(this);
        }
        MusicSource.Play();
    }
    public void PlaySFX(AudioClip _clip)
    {
        SFXSource.clip = _clip;
        SFXSource.Play();
    }
    public void PlayShootSFX(AudioClip _clip)
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
