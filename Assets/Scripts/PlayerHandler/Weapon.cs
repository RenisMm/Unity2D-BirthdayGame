using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public AudioSource audio;
    public GameObject BulletPewpew;
    public GameObject player;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && rb.velocity.x < 1 && rb.velocity.x > -1)
        {
            Instantiate(BulletPewpew, FirePoint.position, FirePoint.rotation);
            audio.Play();
        }
    }
}
