using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : MonoBehaviour
{
    PhysicsComponent physicsComponent;
    StateComponent stateComponent;

    void Start()
    {
        physicsComponent = GetComponent<PhysicsComponent>();
        stateComponent = GetComponent<StateComponent>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GrabbableObject" && physicsComponent.CanGrab())
        {
            physicsComponent.GrabObject(collision);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Orb")
        {
            Destroy(collision.gameObject);
            stateComponent.AddOrb();
        }
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            stateComponent.AddKey();
        }
    }
}
