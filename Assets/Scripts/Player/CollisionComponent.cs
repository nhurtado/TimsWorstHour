using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : MonoBehaviour
{
    PhysicsComponent physicsComponent;

    void Start()
    {
        physicsComponent = GetComponent<PhysicsComponent>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GrabbableObject" && physicsComponent.CanGrab())
        {
            physicsComponent.GrabObject(collision);
        }
    }
}
