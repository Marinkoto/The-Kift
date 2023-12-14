
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
    public DungeonInfo dungeonInfo;
    public DungeonController dungeonController;
    public bool canGetHit;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        PlayerStats.instance.playerHealth = PlayerStats.instance.playerMaxHealth;
        matDef = sr.material;
        playerHealthBar.SetUIHealth(PlayerStats.instance.playerHealth, PlayerStats.instance.playerMaxHealth);
        canGetHit = true;
    }
    public void TakeDamage(float damage)
    {
        if (canGetHit)
        {
            AudioManager.instance.PlayHitSFX(AudioManager.instance.playerHit);
            if (!LoadingScreeen.loadingScreenON)
            {
                PlayerStats.instance.playerHealth -= damage / PlayerStats.instance.armor;
                ShowDamage(Mathf.RoundToInt(damage / PlayerStats.instance.armor).ToString());
            }
            CameraShake.instance.ShakeCamera(0.2f, 0.6f);
            sr.material = matWhite;
            Invoke("ResetMaterial", 0.16f);
            if (!PlayerStats.instance)
            {
                return;
            }
            if (PlayerStats.instance.playerHealth <= 0)
            {
                if (dungeonController.dungeonNumber < 3 && dungeonInfo.dungeonLevel == 1)
                {
                    int tokens = Random.Range(0, 5);
                    PlayfabManager.instance.AddCurrency(tokens);
                    dungeonInfo.EnableLoseScreen(tokens);
                }
                if (dungeonController.dungeonNumber > 3 && dungeonInfo.dungeonLevel == 1)
                {
                    int tokens = Random.Range(20, 30);
                    PlayfabManager.instance.AddCurrency(tokens);
                    dungeonInfo.EnableLoseScreen(tokens);
                }
                if (dungeonInfo.dungeonLevel == 2)
                {
                    int tokens = Random.Range(40, 50);
                    PlayfabManager.instance.AddCurrency(tokens);
                    dungeonInfo.EnableLoseScreen(tokens);
                }
                if (dungeonInfo.dungeonLevel == 3)
                {
                    int tokens = Random.Range(60, 70);
                    PlayfabManager.instance.AddCurrency(tokens);
                    dungeonInfo.EnableLoseScreen(tokens);
                }
            }
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
