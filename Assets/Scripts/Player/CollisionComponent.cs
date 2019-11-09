﻿using System.Collections;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!hasBeenDamagedRecently)
            {
                hasBeenDamagedRecently = true;
                Physics2D.IgnoreLayerCollision(8, 9, true);
                if (collision.gameObject.transform.position.x <= transform.position.x)
                {
                    physicsComponent.JumpByDamage(true);
                }
                else
                {
                    physicsComponent.JumpByDamage(false);
                }
                stateComponent.RecieveDamage(1);
                StartCoroutine("ResetHasBeingDamagedByEnemy");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
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
                stateComponent.RecieveDamage(1);
                StartCoroutine("ResetHasFallenToVoid");
            }
            rb.velocity = new Vector2(0f, 0f);
            rb.position = stateComponent.iniPosition;
        }
    }
    
    IEnumerator ResetHasFallenToVoid()
    {
        yield return new WaitForSeconds(0.5f);
        hasBeenDamagedRecently = false;
    }

    IEnumerator ResetHasBeingDamagedByEnemy()
    {
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        hasBeenDamagedRecently = false;
    }


}
