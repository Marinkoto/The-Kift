using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    private Transform player;
    public ClassesHandler classes;


    private void Start()
    {
        player = classes.classSelected.GetComponent<Transform>();
    }
    void Update()
    {
        Vector2 position = new Vector2(player.position.x, player.position.y);
        transform.position = position;
    }
}
