using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
    public float jumpPower = 4;
    public float speed = 7;
    Dictionary<string, bool> inputs;

    bool facingRight = true;

    bool isGrabbingObject = false;
    GameObject grabbedObject;

    private GameObject iceballcheck;
    public GameObject iceBallPrefab;
    public Transform iceBallSpawner;
    GameObject newIceBall;

    Rigidbody2D rb;
    StateComponent stateComponent;
    FreezeWorldComponent worldComponent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateComponent = GetComponent<StateComponent>();
        stateComponent.iniPosition = rb.position;
        GameObject go = GameObject.Find("TheWorld");
        worldComponent = go.GetComponent<FreezeWorldComponent>();
    }

    public void ProcessPlayerInputs(float moving, Dictionary<string, bool> keys, bool facingRight)
    {
        inputs = keys;
        this.facingRight = facingRight;
        if (moving != 0)
        {
            MovePlayer(moving);
        }
        if (inputs["Jump"] && stateComponent.isGrounded)
        {
            PlayerJump();
        }
        if (inputs["F"])
        {
            ReleaseObject(false);
        }
        if (inputs["R"])
        {
            ReleaseObject();
        }
        if (inputs["T"])
        {
            StopTime();
        }
        if (inputs["Z"])
        {
            IceBall();
        }
        if (inputs["C"])
        {
            ChangeEra();
        }
    }

    void MovePlayer(float moving)
    {
        rb.velocity = new Vector2(moving * speed, rb.velocity.y);
    }

    void PlayerJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }

    public void GrabObject(Collision2D collision)
    {
        grabbedObject = collision.gameObject;
        grabbedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        grabbedObject.transform.parent = this.transform;
        grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
        if (facingRight)
        {
            grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position + new Vector2(1.5f, 0.35f);
        }
        else
        {
            grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position + new Vector2(-1.5f, 0.35f);
        }
        grabbedObject.GetComponent<Rigidbody2D>().mass = 1;
        isGrabbingObject = true;
    }

    void ReleaseObject(bool throwObject = true)
    {
        if (isGrabbingObject)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            if (throwObject)
            {
                if (facingRight)
                {
                    grabbedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 4), ForceMode2D.Impulse);
                }
                else
                {
                    grabbedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 4), ForceMode2D.Impulse);
                }
            }
            grabbedObject.GetComponent<Rigidbody2D>().mass = 10;
            grabbedObject = null;
            isGrabbingObject = false;
        }
    }

    void StopTime()
    {
        FreezeWorldComponent.instance.FreezeTime();
    }

    void ChangeEra()
    {
        EraChangeWorldComponent.instance.TriggerEraChange();
    }

    public void JumpByDamage(bool jumpRight)
    {
        if (jumpRight)
        {
            rb.velocity = new Vector2(3,6);
        }
        else
        {
            rb.velocity = new Vector2(-3,6);
        }
        
    }

    void IceBall()
    {
        iceballcheck = GameObject.FindGameObjectWithTag("IceBall");
        if (iceballcheck == null)
        {
            newIceBall = Instantiate(iceBallPrefab, iceBallSpawner.position, iceBallSpawner.rotation);
        }  
    }

    public bool CanGrab()
    {
        return inputs["E"] && !isGrabbingObject;
    }

    public bool CanExit() {
        return inputs["Up"] || inputs["W"];
    }
}
