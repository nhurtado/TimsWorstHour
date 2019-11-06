using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FreezeComponent : MonoBehaviour
{
    Vector2 savedVelocity;
    Rigidbody2D rb;

    void Start()
    {
        FreezeWorldComponent.FreezeEvent += FreezeObject;
        FreezeWorldComponent.UnfreezeEvent += UnfreezeObject;
        rb = GetComponent<Rigidbody2D>();
    }

    void FreezeObject()
    {
        if (!rb.isKinematic)
        {
            savedVelocity = rb.velocity;
        }
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    void UnfreezeObject()
    {
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        if (!rb.isKinematic)
        {
            rb.velocity = savedVelocity;
        }
    }
}
