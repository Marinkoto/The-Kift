using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Net;
using UnityEngine.Events;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance { get; private set; }
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public int currentAmmo;
    public float classReloadTime;
    public GameObject slider;
    bool canReload = true;
    public bool canShoot = true;
    public UnityEvent<float> OnReloading;
    public AudioClip classFireSound;
    public float spread;
    public TextMeshProUGUI bulletCounter;
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
        slider.gameObject.SetActive(false);
    }
    private void Start()
    {
        OnReloading?.Invoke(PlayerStats.instance.reloadTime);
        PlayerStats.instance.SetBulletCounter(bulletCounter);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && canReload && PlayerStats.instance.maxClipSize != PlayerStats.instance.currentClip && !PauseMenu.isPaused)
        {
            slider.gameObject.SetActive(true);
            canShoot = false;
            StartCoroutine(Reload());

        }
        if (PlayerStats.instance.currentClip == 0 && canReload)
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
            PlayerStats.instance.reloadTime -= Time.deltaTime;
            OnReloading?.Invoke(PlayerStats.instance.reloadTime);
            if (PlayerStats.instance.reloadTime <= 0 && canReload==false)
            {
                PlayerStats.instance.reloadTime = classReloadTime;
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
        for (int i = 0; i < PlayerStats.instance.bulletAmount; i++)
        {
            if (PlayerStats.instance.currentClip > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(classFireSound);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 dir = firePoint.transform.right;
                Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
                rb.AddForce((dir+pdir)*bulletForce, ForceMode2D.Impulse);
                PlayerStats.instance.currentClip--;
                PlayerStats.instance.SetBulletCounter(bulletCounter);
            }
        }
    }
    public IEnumerator Reload()
    {
        canShoot = false;
        canReload = false;
        yield return new WaitForSeconds(PlayerStats.instance.reloadTime);
        int reloadAmount = PlayerStats.instance.maxClipSize - PlayerStats.instance.currentClip;
        reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : 0;
        PlayerStats.instance.currentClip += reloadAmount;
        canReload = true;
        canShoot = true;
        slider.gameObject.SetActive(false);
        PlayerStats.instance.SetBulletCounter(bulletCounter);
    }
    
}
