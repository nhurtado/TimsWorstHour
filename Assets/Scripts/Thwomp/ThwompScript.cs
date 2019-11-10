using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompScript : MonoBehaviour
{
    Vector2 iniPosition;
    Rigidbody2D rb;
    bool goingUp = false;
    public float speed = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        iniPosition = rb.position;
    }
    
    void Update()
    {

        if (goingUp)
        {
            if (rb.position.y >= iniPosition.y)
            {
                goingUp = false;
            }
            else
            {
                rb.velocity = new Vector2(0, speed);
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            StartCoroutine("DelayGoingUp");
        }
    }

    IEnumerator DelayGoingUp()
    {
        yield return new WaitForSeconds(2f);
        goingUp = true;
    }
}
