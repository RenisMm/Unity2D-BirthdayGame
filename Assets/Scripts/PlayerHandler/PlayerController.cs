using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpStr;
    public float health;
    public int numOfHearts;

    public GameObject tryAgainUI;
    public GameObject Firepoint;
    public GameObject pauseMenuUI;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    public AudioSource healthUpSound;
    public AudioSource hurtSound;
    public AudioSource jumpSound;

    public Image[] hearts;

    [SerializeField] LayerMask ground;
    public static bool isGamePaused = false;

    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    private Object explosionRef;


    void Start()
    {
        //BGSound.Instance.gameObject.GetComponent<AudioSource>().Pause();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        explosionRef = Resources.Load("Explosion");

    }

    void Update()
    {
       Movement();
       Jump();
       HealthManager();
       PausingGame();
    }


    void Movement()
    {
        
        float xdirection = Input.GetAxis("Horizontal");
        if (xdirection < 0)
        {
            anim.Play("DianaRun");
            rb.velocity = new Vector2(-1f * speed, rb.velocity.y);
            transform.localScale = new Vector2(-0.8f, 0.8f);
            Firepoint.transform.eulerAngles = Vector3.forward * 180;
        }
        else if (xdirection > 0)
        {

            anim.Play("DianaRun");
            rb.velocity = new Vector2(1f * speed, rb.velocity.y);
            transform.localScale = new Vector2(0.8f, 0.8f);
            Firepoint.transform.rotation = Quaternion.Euler(Vector3.right * 180);
        }
        else if (Input.GetButton("Fire1"))
        {
            anim.Play("DianaShoot");
        }
        else
        {
            anim.Play("DianaIdle");
            rb.velocity = new Vector3(0, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {//jump ako layer-a e ground
            rb.velocity = new Vector2(rb.velocity.x, jumpStr);
            jumpSound.Play();
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {//jump ako layer-a e ground
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpStr);
        }
    }

    void PausingGame()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
                isGamePaused = true;
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GetComponent<Weapon>().enabled = false;
                GetComponent<PlayerController>().enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {

        health -= damage;
        hurtSound.Play();

        if (health <= 0)
        {
            Death();
        }
    }

    void HealthManager()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
               hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bunny"))
        {
           Physics2D.IgnoreCollision(coll, collision.collider);
        }
        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            jumpStr = 8.5f;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ogunche"))
        {
            health-= 1f;
            hurtSound.Play();
        }
        if (collision.gameObject.CompareTag("goldenHeart"))
        {
            if (health < 10)
            {
                healthUpSound.Play();
                health++;
            }
            Destroy(collision.gameObject);
        }
        

        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Lava"))
        {
            Death();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            jumpStr = 6;
        }
    }

    void Death()
    {
        gameObject.SetActive(false);
        tryAgainUI.SetActive(true);
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
    }
}