using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTest : MonoBehaviour
{
    public float speed;
    public float distanceToJump;
    public Transform target;
    public LayerMask ground;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Vector2 difference;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        followMovement();
    }

    void followMovement()
    {
        if (Vector2.Distance(transform.position, target.position) > 3)
        {
            if (Vector2.Distance(transform.position, target.position) > 5)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, target.position) > distanceToJump && coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(0, 5);
            }
            if (Vector2.Distance(transform.position, target.position) > 15)
            {
                if (transform.position.x > target.position.x)
                {
                    transform.position = new Vector3(target.position.x + 1.5f, target.position.y + 1.17f, target.position.z);
                }
                if (transform.position.x < target.position.x)
                {
                    transform.position = new Vector3(target.position.x - 1.5f, target.position.y + 1.17f, target.position.z);
                }
            }
            if (target.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bunny"))
        {
            Physics2D.IgnoreCollision(coll, collision.collider);
        }
    }

    private void OnMouseDown()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }
}
