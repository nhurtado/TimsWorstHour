using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckComponent : MonoBehaviour
{
    StateComponent stateComponent;
    void Start()
    {
        stateComponent = GetComponentInParent<StateComponent>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        stateComponent.isGrounded = true;
        if (collision.gameObject.transform.tag == "Platform")
        {
            transform.parent.parent = collision.gameObject.transform;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        stateComponent.isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        stateComponent.isGrounded = false;
        if (collision.gameObject.transform.tag == "Platform")
        {
            transform.parent.parent = null;
        }
    }
}
