using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Axe : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private bool isRotating;
    private bool isClicked;
    private bool isDamaged;

    [SerializeField] public float moveSpeed;
    private Vector3 targetPos;

    private Transform player;
    private bool canCallBack;
    private bool returnWeapon;
    public Transform aimTransform;
    private PlayerMovement playerMovement;
    private bool canMove;
    public GameObject hitEffect;
    public Vector3 offset;
    private void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        transform.position = aimTransform.position;
    }

    private void Update()
    {
        SelfRotation();
        if (Input.GetMouseButtonDown(0)&&!isClicked)
        {
            isClicked = true;
            AudioManager.instance.PlaySFX(AudioManager.instance.axeSwoosh);
            targetPos = UtilsClass.GetMouseWorldPosition();
        }
        if (isClicked)
        {
            ThrowWeapon();
        }
        if (Vector2.Distance(transform.position, targetPos) <= 0.01f)
        {
            isRotating = false;
            isDamaged = false;
            canCallBack = true;
        }
        if (canCallBack)
        {
            isDamaged = true;
            returnWeapon = true;
            isRotating = false;
            canMove = true;
        }
        if (returnWeapon && canMove)
        {
            CallBackWeapon();
            
        }
        if (Vector2.Distance(transform.position,player.position + offset)<=0.01f)
        {
            isRotating = false;
            canCallBack = false;
            returnWeapon = false;
            isDamaged = false;
            isClicked = false;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    private void CallBackWeapon()
    {
        isRotating = true;
        transform.position = Vector2.MoveTowards(transform.position, player.position + offset, moveSpeed*3 * Time.deltaTime);
    }
    private void ThrowWeapon()
    {
        isRotating = true;
        isDamaged = true;
        transform.position=Vector2.MoveTowards(transform.position, targetPos, moveSpeed*Time.deltaTime);
    }
    private void SelfRotation()
    {
        if (isRotating)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy)&&isDamaged)
        {
            isDamaged = true;
            AudioManager.instance.PlayHitSFX(AudioManager.instance.enemyHit);
            enemy.TakeDamage(PlayerStats.instance.playerDamage);

        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isDamaged = true;
            returnWeapon = true;
            isRotating = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position + offset, moveSpeed * 3 * Time.deltaTime);
        }
        if (collision.gameObject.TryGetComponent<Item>(out Item item) && !item.nonDestructible && isDamaged)
        {
            isDamaged = true;
            returnWeapon = true;
            isRotating = false;
            Destroy(collision.gameObject);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }
}
