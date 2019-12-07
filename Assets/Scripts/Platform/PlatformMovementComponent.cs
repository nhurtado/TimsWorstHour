using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementComponent : MonoBehaviour
{
    Vector2 initialPosition;
    public bool goingUpRight = true;
    public float initialSpeed = 1.5f;
    public float speed;
    public bool movingVertically = true;
    public float upperLimit = 2;
    public float lowerLimit = 2;

    void Start()
    {
        initialPosition = transform.position;
        FreezeWorldComponent.FreezeEvent += FreezePlatform;
        FreezeWorldComponent.UnfreezeEvent += UnfreezePlatform;
        speed = initialSpeed;
    }

    void FixedUpdate()
    {
        Move();
        CheckBoundsAndChangeDirection();
    }

    void Move()
    {
        if (movingVertically)
        {
            if (goingUpRight)
            {
                transform.Translate(new Vector2(0, Time.deltaTime * speed));
            }
            else
            {
                
                transform.Translate(new Vector2(0, -Time.deltaTime * speed));
            }  
        }
        else
        {
            if (goingUpRight)
            {
                transform.Translate(new Vector2(Time.deltaTime * speed, 0));
            }
            else
            {
                transform.Translate(new Vector2(-Time.deltaTime * speed, 0));
            }
        }
    }

    void CheckBoundsAndChangeDirection()
    {
        if (movingVertically)
        {
            if (transform.position.y > initialPosition.y + upperLimit || transform.position.y < initialPosition.y - lowerLimit)
            {
                goingUpRight = !goingUpRight;
            }
        }
        else
        {
            if (transform.position.x > initialPosition.x + upperLimit || transform.position.x < initialPosition.x - lowerLimit)
            {
                goingUpRight = !goingUpRight;
            }
        }
    }

    void FreezePlatform()
    {
        speed = 0;
    }

    void UnfreezePlatform()
    {
        speed = initialSpeed;
    }
}
