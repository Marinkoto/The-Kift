
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer sr;
    public Material matWhite;
    private Material matDef;
    public PlayerHealthBar playerHealthBar;
    public GameObject floatingTextPrefab;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        PlayerStats.instance.playerHealth = PlayerStats.instance.playerMaxHealth;
        matDef = sr.material;
        playerHealthBar.SetUIHealth(PlayerStats.instance.playerHealth, PlayerStats.instance.playerMaxHealth);
    }
    public void TakeDamage(float damage)
    {
        AudioManager.instance.PlayHitSFX(AudioManager.instance.playerHit);
        if (!LoadingScreeen.loadingScreenON)
        {
            PlayerStats.instance.playerHealth -= damage / PlayerStats.instance.armor;
            ShowDamage(Mathf.RoundToInt(damage).ToString());
        }
        CameraShake.instance.ShakeCamera(0.2f, 0.6f);
        sr.material = matWhite;
        if (!PlayerStats.instance)
        {
            return;
        }
        if (PlayerStats.instance.playerHealth <= 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Invoke("ResetMaterial", .15f);
        }
    }
    private void Update()
    {
        playerHealthBar.SetUIHealth(PlayerStats.instance.playerHealth, PlayerStats.instance.playerMaxHealth);
    }
    void ResetMaterial()
    {
        sr.material = matDef;
    }
    void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }
    }
}
