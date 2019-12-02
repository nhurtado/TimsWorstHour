using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    public int healthPoints = 3;
    public float speed = 2.5f;
    public float rightLimit = 5f;
    public float leftLimit = 5f;
    bool facingRight = false;
    public bool flyingVertical = false;
    public bool goingUp = false;
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
        if (flyingVertical)
        {
            MoveVertical();
        }
        else
        {
            MoveHorizontal();
        }
    }

    void MoveVertical()
    {
        if (goingUp)
        {
            rb.velocity = new Vector2(0, speed);
        }
        else
        {
            rb.velocity = new Vector2(0, -speed);
        }
    }

    void MoveHorizontal()
    {
        if (facingRight)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    void CheckBoundsAndChangeDirection()
    {
        if (flyingVertical)
        {
            CheckVertical();
        }
        else
        {
            CheckHorizontal();
        }
    }

    void CheckVertical()
    {
        if (rb.position.y > initialPosition.y + rightLimit)
        {
            goingUp = false;
        }
        else if (rb.position.y < initialPosition.y - leftLimit)
        {
            goingUp = true;
        }
    }

    void CheckHorizontal()
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
