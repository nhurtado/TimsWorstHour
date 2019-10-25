using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
    
    public float jumpPower = 4;
    public float speed = 7;
    Dictionary<string, bool> inputs;

    Rigidbody2D rb;
    bool facingRight = true;

    bool isGrabbingObject = false;
    GameObject grabbedObject;

    bool isTimeStopped = false;
    float lastTimeStop = float.MaxValue;

    GameObject[] blocks;
    List<Vector2> blocksVelocities = new List<Vector2>();

    StateComponent stateComponent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateComponent = GetComponent<StateComponent>();
    }

    public void ProcessPlayerInputs(float moving, Dictionary<string, bool> keys, bool facingRight)
    {
        if (isTimeStopped && lastTimeStop + 3f <= Time.time)
        {
            ResumeTime();
        }
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
            grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position + new Vector2(1f, 0.35f);
        }
        else
        {
            grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position + new Vector2(-1f, 0.35f);
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
        if (!isTimeStopped)
        {
            blocks = GameObject.FindGameObjectsWithTag("GrabbableObject");
            foreach (GameObject block in blocks)
            {
                blocksVelocities.Add(block.GetComponent<Rigidbody2D>().velocity);
                block.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            isTimeStopped = true;
            lastTimeStop = Time.time;
        }
    }

    void ResumeTime()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            if (!blocks[i].GetComponent<Rigidbody2D>().isKinematic)
            {
                blocks[i].GetComponent<Rigidbody2D>().velocity = blocksVelocities[i];
            }
        }
        blocksVelocities.Clear();
        isTimeStopped = false;
    }

    public bool CanGrab()
    {
        return inputs["E"] && !isGrabbingObject;
    }
}
