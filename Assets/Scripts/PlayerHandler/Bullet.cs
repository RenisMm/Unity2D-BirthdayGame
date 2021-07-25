using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 20f;
	public float timeLeft;
	public int damage = 30;
	public GameObject impactEffect;
	public Rigidbody2D rb;

	private AudioSource audio;
	private Object bloodRef;

	void Start()
	{
		rb.velocity = transform.right * speed;
		bloodRef = Resources.Load("Blood");
	}

    private void Update()
    {
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0)
		{
			Destroy(gameObject);
		}
	}

    void OnTriggerEnter2D(Collider2D hitInfo)
	{
		
		if (hitInfo.CompareTag("Enemy"))
		{
			Enemy enemy = hitInfo.GetComponent<Enemy>();
			GameObject blood = (GameObject)Instantiate(bloodRef);
			blood.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

			enemy.TakeDamage(damage);
			GameObject newhit = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(newhit, 0.3f);
		}
        else if (hitInfo.CompareTag("Background") || hitInfo.CompareTag("triggerArea"))
        {
			return;
        }
		else if (hitInfo.CompareTag("BulletDestoyer"))
		{
			Destroy(gameObject);
		}
		else
        {
			Destroy(gameObject);
        }
		Destroy(gameObject);
	}
}