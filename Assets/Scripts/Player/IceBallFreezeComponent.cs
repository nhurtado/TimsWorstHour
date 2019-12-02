using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceBallFreezeComponent : MonoBehaviour
{
    Vector2 savedVelocity;
    Rigidbody2D rb;
    public bool GiveXBack = true;
    public bool GiveYBack = true;
    public bool GiveRotationBack = false;

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
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void UnfreezeObject()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        if (!GiveXBack)
        {
            rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionX;
        }
        if (!GiveYBack)
        {
            rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionY;
        }
        if (!GiveRotationBack)
        {
            rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezeRotation;
        }
        if (!rb.isKinematic)
        {
            rb.velocity = savedVelocity;
        }
    }

    public void Unsubscribe()
    {
        FreezeWorldComponent.FreezeEvent -= FreezeObject;
        FreezeWorldComponent.UnfreezeEvent -= UnfreezeObject;
    }
}
