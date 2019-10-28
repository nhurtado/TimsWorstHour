using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CollisionComponent : MonoBehaviour
{
    PhysicsComponent physicsComponent;
    StateComponent stateComponent;
    Rigidbody2D rb;
    bool hasBeenDamagedRecently = false;

    void Start()
    {
        physicsComponent = GetComponent<PhysicsComponent>();
        stateComponent = GetComponent<StateComponent>();
        rb = GetComponent<Rigidbody2D>();
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
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Door" && physicsComponent.CanExit()) {
            collision.GetComponent<DoorController>().OpenDoor();
        }
        if (collision.tag == "void")
        {
            if (!hasBeenDamagedRecently)
            {
                hasBeenDamagedRecently = true;
                rb.velocity = new Vector2(0f, 0f);
                rb.position = stateComponent.iniPosition;
                stateComponent.RecieveDamage(1);
                StartCoroutine("ResetHasBeenDamagedRecently");
            }
        }
    }
    
    IEnumerator ResetHasBeenDamagedRecently()
    {
        yield return new WaitForSeconds(0.2f);
        hasBeenDamagedRecently = false;
    }
}
