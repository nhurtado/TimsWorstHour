using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyScript : MonoBehaviour
{
    public float speed = 2.5f;
    public float rightLimit = 5f;
    public float leftLimit = 5f;
    public bool facingRight = false;
    Rigidbody2D rb;
    Vector2 initialPosition;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = rb.position;
    }

    void Update()
    {
        Move();
        CheckBoundsAndChangeDirection();
    }

    void Move()
    {
        if (facingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void CheckBoundsAndChangeDirection()
    {
        if (rb.position.x > initialPosition.x + rightLimit)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            facingRight = false;
        }
        else if (rb.position.x < initialPosition.x - leftLimit)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            facingRight = true;
        }
    }
}
