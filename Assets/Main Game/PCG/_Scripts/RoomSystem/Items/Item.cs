using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private BoxCollider2D itemCollider;

    [SerializeField]
    int health = 3;
    [SerializeField]
    public bool nonDestructible;

    [SerializeField]
    private GameObject hitFeedback, destoyFeedback;
    public ParticleSystem ps;
    public UnityEvent OnGetHit { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    private void OnDestroy()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.itemDestroy);
        Instantiate(ps, transform.position+new Vector3(0.5f,0.5f), Quaternion.identity);
    }
    
    public void Initialize(ItemData itemData)
    {
        //set sprite
        spriteRenderer.sprite = itemData.sprite;
        //set sprite offset
        spriteRenderer.transform.localPosition = new Vector2(0.5f * itemData.size.x, 0.5f * itemData.size.y);
        itemCollider.size = itemData.size;
        itemCollider.offset = spriteRenderer.transform.localPosition;
        ps = itemData.ps;

        if (itemData.nonDestructible)
            nonDestructible = true;

        this.health = itemData.health;

    }
}

