using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int heatlh = 100;
    public GameObject enemy;

    private Material matWhite;
    private Material matDefault;
    private Object explosionRef;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
    }

    public void TakeDamage(int damage)
    {
        
        heatlh -= damage;
        sr.material = matWhite;

        if (heatlh <= 0)
        {
            Die();
            Destroy(enemy);
        }
        else
        {
            Invoke("ResetMaterial", 0.1f);
        }
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    void Die()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        Destroy(gameObject);
    }
}
