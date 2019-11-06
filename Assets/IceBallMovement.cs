using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallMovement : MonoBehaviour
{
    private Rigidbody2D iceBallRB;

    public float iceBallSpeed;

    private void Awake()
    {
        iceBallRB = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        iceBallRB.velocity = new Vector2(iceBallSpeed, iceBallRB.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
