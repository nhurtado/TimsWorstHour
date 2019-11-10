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
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            //collision.gameObject.transform.parent = this.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "void") {
            rb.velocity = new Vector2(0f, 0f);
            rb.position = iniPosition;
        }
    }
}
