using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	public int damage = 1;
	public float timer;

	private float intTimer;
    private Collider2D hitboxAxe;

	private void Start()
    {
        intTimer = timer;
        hitboxAxe = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
	{
            if (hitInfo.CompareTag("Player"))
            {
                PlayerController enemy = hitInfo.GetComponent<PlayerController>();
                enemy.TakeDamage(damage);
            }
    }
}
