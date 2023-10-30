using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
    

public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody2D rb;
    private Vector2 movement;

    public float dashDuration = 1f;
    public float dashSpeed = 10f;
    public bool isDashing;
    public Animator anim;
    public ParticleSystem particle;
    public Transform startPos;
    private void Awake()
    {
        transform.position = startPos.position; 
    }
    void Update()
    {
        rb.velocity.Normalize();
        if (isDashing)
        {
            return;
        }
        if (!PauseMenu.isPaused)
        {
            Move();
        }
        if (Input.GetMouseButtonDown(1) && PlayerStats.instance.canDash && !PauseMenu.isPaused)
        {
            StartCoroutine(Dash());
            AudioManager.instance.PlaySFX(AudioManager.instance.playerDash);
            particle.Play();
        }
    }
     
    IEnumerator Dash()
    {
        PlayerStats.instance.canDash = false;
        isDashing = true;
        anim.SetBool("Dash", isDashing);
        Time.timeScale = 0.65f;
        rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        Time.timeScale = 1f;
        anim.SetBool("Dash", isDashing);
        yield return new WaitForSeconds(PlayerStats.instance.dashCooldown);
        PlayerStats.instance.canDash = true;
        
    }
    private void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        
        rb.velocity = new Vector2(movement.x * PlayerStats.instance.playerMoveSpeed, movement.y * PlayerStats.instance.playerMoveSpeed);
        
    }
}


