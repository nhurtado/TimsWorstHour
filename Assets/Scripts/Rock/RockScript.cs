using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    Vector2 iniPosition;
    Rigidbody2D rb;
    public float respawnTime = 5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        iniPosition = rb.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "void")
        {
            GoToSpawn();
        }
    }

    void GoToSpawn()
    {
        rb.velocity = new Vector2(0f, 0f);
        rb.position = iniPosition;
    }
    
}
