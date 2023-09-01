using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using System.Net;
using UnityEngine.Events;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance { get; private set; }
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public int currentClip, maxClipSize = 10, currentAmmo;
    public float reloadTime;
    public float classReloadTime;
    public GameObject slider;
    bool canReload = true;
    public bool canShoot = true;
    public UnityEvent<float> OnReloading;
    private float spikeBallRate = 10f;
    public bool splashable = false;
    public GameObject splashDamageBullet;
    public AudioClip classFireSound;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        slider.gameObject.SetActive(true);
    }
    private void Start()
    {
        splashable = false;
        OnReloading?.Invoke(reloadTime);
        spikeBallRate = 0;
    }
    private void Update()
    {
        if (spikeBallRate<=0)
        {
            spikeBallRate = 10f;
            if (splashable && Input.GetKeyDown(KeyCode.Q))
            {
                spikeBallRate -= Time.deltaTime;
                Instantiate(splashDamageBullet, firePoint.position, firePoint.rotation);
            }
        }
       
        if (Input.GetKeyDown(KeyCode.R) && canReload && maxClipSize!=currentClip && !PauseMenu.isPaused)
        {
            slider.gameObject.SetActive(true);
            canShoot = false;
            StartCoroutine(Reload());

        }
        if (currentClip == 0 && canReload)
        {
            canShoot = false;
            slider.gameObject.SetActive(true);
            StartCoroutine(Reload());
        }
        if (Input.GetMouseButtonDown(0) && canShoot&&!PauseMenu.isPaused)
        {
            Shoot();
        }
        if (canShoot==false)
        {
            canReload = false;
            reloadTime -= Time.deltaTime;
            OnReloading?.Invoke(reloadTime);
            if (reloadTime <=0 && canReload==false)
            {
                reloadTime = classReloadTime;
                canShoot = true;
            }
        }
        else
        {
            return;
        }
    }
    private void Shoot()
    {
        if (currentClip > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            AudioManager.instance.PlaySFX(classFireSound);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            currentClip--;
        }
    }
    public IEnumerator Reload()
    {
        canShoot = false;
        canReload = false;
        yield return new WaitForSeconds(reloadTime);
        int reloadAmount = maxClipSize - currentClip;
        reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : 0;
        currentClip += reloadAmount;
        canReload = true;
        canShoot = true;
    }
    
}
