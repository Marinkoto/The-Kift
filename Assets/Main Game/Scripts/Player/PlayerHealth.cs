
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer sr;
    public Material matWhite;
    private Material matDef;
    public PlayerHealthBar playerHealthBar;
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
        PlayerStats.instance.playerHealth -= damage;
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
}
