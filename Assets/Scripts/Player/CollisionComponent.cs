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
    bool hasTriggeredRecently = false;


    void Start()
    {
        physicsComponent = GetComponent<PhysicsComponent>();
        stateComponent = GetComponent<StateComponent>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 11, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Hazard")
        {
            if (!hasBeenDamagedRecently)
            {
                hasBeenDamagedRecently = true;
                Physics2D.IgnoreLayerCollision(8, 9, true);
                Physics2D.IgnoreLayerCollision(8, 11, true);
                stateComponent.RecieveDamage(1);
                if (collision.gameObject.transform.position.x <= transform.position.x)
                {
                    physicsComponent.JumpByDamage(true);
                }
                else
                {
                    physicsComponent.JumpByDamage(false);
                }
                StartCoroutine("ResetHasBeingDamaged");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggeredRecently)
        {
            hasTriggeredRecently = true;
            if (collision.gameObject.tag == "Orb")
            {
                Destroy(collision.gameObject);
                stateComponent.AddOrb();
            }
            if (collision.gameObject.tag == "Upper")
            {
                Destroy(collision.gameObject);
                FreezeWorldComponent.instance.UpgradeFreeze();
            }
            if (collision.gameObject.tag == "Downer")
            {
                Destroy(collision.gameObject);
                FreezeWorldComponent.instance.UpgradeCooldown();
            }
            if (collision.gameObject.tag == "Key")
            {
                Destroy(collision.gameObject);
                stateComponent.AddKey();
            }
            if (collision.gameObject.tag == "DialogueTrigger")
            {
                collision.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
            StartCoroutine("ResetHasTriggered");
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Door" && physicsComponent.CanExit()) {
            if (physicsComponent.isGrabbingObject)
            {
                physicsComponent.ReleaseObject(false);
            }
            collision.GetComponent<DoorController>().OpenDoor();
        }
        if (collision.tag == "void")
        {
            if (!hasBeenDamagedRecently)
            {
                hasBeenDamagedRecently = true;
                stateComponent.RecieveDamage(1);
                StartCoroutine("ResetHasFallenToVoid");
            }
            if (physicsComponent.isGrabbingObject)
            {
                physicsComponent.ReleaseObject(false);
            }
            rb.velocity = new Vector2(0f, 0f);
            rb.position = stateComponent.iniPosition;
        }
    }

    IEnumerator ResetHasTriggered()
    {
        yield return new WaitForSeconds(0.02f);
        hasTriggeredRecently = false;
    }

    IEnumerator ResetHasFallenToVoid()
    {
        yield return new WaitForSeconds(0.5f);
        hasBeenDamagedRecently = false;
    }

    IEnumerator ResetHasBeingDamaged()
    {
        yield return new WaitForSeconds(2.5f);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 11, false);
        hasBeenDamagedRecently = false;
    }


}
