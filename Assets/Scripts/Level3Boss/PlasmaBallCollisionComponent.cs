using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBallCollisionComponent : MonoBehaviour
{
    public Vector2 vec;
    public float speed = 10f;
    public float initialSpeed = 10f;

    void Start()
    {
        FreezeWorldComponent.FreezeEvent += FreezeObject;
        FreezeWorldComponent.UnfreezeEvent += UnfreezeObject;
    }

    void Update()
    {
        transform.Translate(vec*Time.deltaTime*speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Untagged")
        {
            Destroy(gameObject);
        }
    }

    void FreezeObject()
    {
        speed = 0;
    }

    void UnfreezeObject()
    {
        speed = initialSpeed;
    }
}
