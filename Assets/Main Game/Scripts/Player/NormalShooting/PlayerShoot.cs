using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Net;
using UnityEngine.Events;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private PlayerMovement playerMovement;
    public float classReloadTime;
    public GameObject slider;
    bool canReload = true;
    public UnityEvent<float> OnReloading;
    public AudioClip classFireSound;
    public float spread;
    public TextMeshProUGUI bulletCounter;
    public bool isHeld;
    public ReloadAnimation reloadController;
    private void Awake()
    {
        slider.gameObject.SetActive(false);
    }
    private void Start()
    {
        OnReloading?.Invoke(PlayerStats.instance.reloadTime);
        PlayerStats.instance.SetBulletCounter(bulletCounter);
        playerMovement = GetComponent<PlayerMovement>();
        PlayerStats.instance.canShoot = true;
        PlayerStats.instance.reloadTime = classReloadTime;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && canReload && PlayerStats.instance.maxClipSize != PlayerStats.instance.currentClip && !PauseMenu.isPaused)
        {
            slider.gameObject.SetActive(true);
            PlayerStats.instance.canShoot = false;
            StartCoroutine(Reload());

        }
        if (PlayerStats.instance.currentClip == 0 && canReload)
        {
            PlayerStats.instance.canShoot = false;
            slider.gameObject.SetActive(true);
            StartCoroutine(Reload());
        }
        if (Input.GetMouseButtonDown(0) && PlayerStats.instance.canShoot && !PauseMenu.isPaused 
            && !playerMovement.isDashing && !DungeonInfo.onEnd && !isHeld && !LoadingScreeen.loadingScreenON)
        {
            Shoot();
        }
        if (Input.GetMouseButtonDown(0) && PlayerStats.instance.canShoot && !PauseMenu.isPaused
            && !playerMovement.isDashing && !DungeonInfo.onEnd && isHeld && !LoadingScreeen.loadingScreenON)
        {
            InvokeRepeating("Shoot", 0.001f, PlayerStats.instance.fireRate);
        }
        else if (Input.GetMouseButtonUp(0) && !PauseMenu.isPaused && !playerMovement.isDashing && isHeld || LoadingScreeen.loadingScreenON)
        {
            CancelInvoke("Shoot");
        }
        else if (PauseMenu.isPaused && DungeonInfo.onEnd)
        {
            CancelInvoke("Shoot");
        }
        if (PlayerStats.instance.canShoot == false)
        {
            CancelInvoke("Shoot");
            canReload = false;
            PlayerStats.instance.reloadTime -= Time.deltaTime;
            OnReloading?.Invoke(PlayerStats.instance.reloadTime);
            if (PlayerStats.instance.reloadTime <= 0 && canReload==false)
            {
                PlayerStats.instance.reloadTime = classReloadTime;
                PlayerStats.instance.canShoot = true;
            }
        }
    }
    private void Shoot()
    {
        for (int i = 0; i < PlayerStats.instance.bulletAmount; i++)
        {
            if (PlayerStats.instance.currentClip > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlayShootSFX(classFireSound);
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
        PlayerStats.instance.canShoot = false;
        canReload = false;
        reloadController.StartCoroutine(reloadController.Reload(PlayerStats.instance.reloadTime - 0.25f));
        yield return new WaitForSeconds(PlayerStats.instance.reloadTime);
        int reloadAmount = PlayerStats.instance.maxClipSize - PlayerStats.instance.currentClip;
        reloadAmount = (PlayerStats.instance.currentAmmo - reloadAmount) >= 0 ? reloadAmount : 0;
        PlayerStats.instance.currentClip += reloadAmount;
        canReload = true;
        PlayerStats.instance.canShoot = true;
        slider.gameObject.SetActive(false);
        PlayerStats.instance.SetBulletCounter(bulletCounter);
    }
    
}
