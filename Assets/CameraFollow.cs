using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;


    void Update()
    {
        transform.position = new Vector2(player.position.x, player.position.y);    
    }
}
