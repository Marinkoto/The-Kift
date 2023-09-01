using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform firePoint;
    public CircleCollider2D circleCollider;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);
        lineRenderer.SetPosition(0,firePoint.position);
        if (Input.GetMouseButtonDown(0) && !PauseMenu.isPaused)
        {
            EnableLaser();
            SetCollider();
            Invoke("DisableLaser", 0.05f);
        }
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, UtilsClass.GetMouseWorldPosition());
        }
    }
    private void EnableLaser()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.shootLaser);
        lineRenderer.enabled = true;
    }
    private void DisableLaser()
    {     
        lineRenderer.enabled = false;
    }
    private void SetCollider()
    {
        Vector3 endPos = lineRenderer.GetPosition(1);
        Instantiate(circleCollider, endPos, Quaternion.identity);
    }
    
}
