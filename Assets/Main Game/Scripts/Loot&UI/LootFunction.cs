using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LootFunction : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject textPopUp;
    private float amountToHeal;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerStats playerStats))
        {
            amountToHeal = Random.Range(15, 35);
            if (playerStats.playerHealth <= playerStats.playerMaxHealth - amountToHeal)
            {
                playerStats.playerHealth += amountToHeal;
                PlaySound();
                Particle();
                ShowHealth("+"+amountToHeal.ToString());
            }
            else
            {
                playerStats.playerHealth = playerStats.playerMaxHealth;
                PlaySound();
                Particle();
                ShowHealth("+"+amountToHeal.ToString());
            }
            Destroy(gameObject);
        }
    }
    private void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.healthPotion);
    }
    private void Particle()
    {
        Instantiate(particle,transform.position, Quaternion.identity);
    }
    void ShowHealth(string text)
    {
        if (textPopUp)
        {
            GameObject prefab = Instantiate(textPopUp, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }
    }
}
