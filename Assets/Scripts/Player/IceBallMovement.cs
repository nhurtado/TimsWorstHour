using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallMovement : MonoBehaviour
{
    private Rigidbody2D iceBallRB;
    public float iceBallSpeed;
    IceBallFreezeComponent iceBallFreezeComponent;
    public int direction = 0;

    private void Awake()
    {
        iceBallRB = GetComponent<Rigidbody2D>();
        iceBallFreezeComponent = GetComponent<IceBallFreezeComponent>();
    }
    
    void Start()
    {
        if (direction == 0)
        {
            iceBallRB.velocity = new Vector2(0, iceBallSpeed);
        }
        else if(direction == 1)
        {
            iceBallRB.velocity = new Vector2(iceBallSpeed, 0);
        }
        else
        {
            iceBallRB.velocity = new Vector2(-iceBallSpeed, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        iceBallFreezeComponent.Unsubscribe();
        Destroy(gameObject);
    }
}
