using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCollisionComponent : MonoBehaviour
{
    public Vector2 vec;

    void Update()
    {
        transform.Translate(vec*Time.deltaTime*10);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Untagged")
        {
            Destroy(gameObject);
        }
    }
}
