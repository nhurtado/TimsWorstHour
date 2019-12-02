using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    Vector2 iniPosition;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        iniPosition = rb.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "void") {
            rb.velocity = new Vector2(0f, 0f);
            rb.position = iniPosition;
        }
    }
}
