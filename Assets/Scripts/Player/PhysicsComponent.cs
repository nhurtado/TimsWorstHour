using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
    public float jumpPower = 4;
    public float speed = 7;
    Dictionary<string, bool> inputs;

    bool facingRight = true;
    bool facingWall = false;

    bool isGrabbingObject = false;
    GameObject grabbedObject;

    private GameObject iceballcheck;
    public GameObject iceBallPrefab;
    public Transform iceBallSpawner;
    GameObject newIceBall;

    public Transform wallDetectionPoint;
    public Transform grabObjectDetectionPoint;

    Rigidbody2D rb;
    StateComponent stateComponent;
    FreezeWorldComponent worldComponent;

    float lastDoorUseTime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateComponent = GetComponent<StateComponent>();
        stateComponent.iniPosition = rb.position;
        GameObject theWorld = GameObject.Find("TheWorld");
        worldComponent = theWorld.GetComponent<FreezeWorldComponent>();
    }

    void Update()
    {
        Vector3 right = wallDetectionPoint.TransformDirection(Vector2.right) * 0.1f;
        Vector3 up = wallDetectionPoint.TransformDirection(Vector2.up) * 0.1f;
        facingWall = false;
        for (int i = 0; i < 20; i++)
        {
            Debug.DrawRay(wallDetectionPoint.position + up * i, right, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(wallDetectionPoint.position + up * i, right, 0.05f);
            if (hit)
            {
                if (hit.transform.tag != "Enemy" && hit.transform.tag != "DialogueTrigger" && hit.transform.tag != "Door" && hit.transform.tag != "GrabbableObject")
                {
                    facingWall = true;
                    break;
                }
            }
        }
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
        if (inputs["E"])
        {
            GrabObject();
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
        if (facingWall)
        {
            if (facingRight && moving > 0 )
            {
                moving = -0.025f;
            }
            else if (!facingRight && moving < 0)
            {
                moving = 0.025f;
            }
        }
        rb.velocity = new Vector2(moving * speed, rb.velocity.y);
    }

    void PlayerJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }

    public void GrabObject()
    {
        Vector3 right = grabObjectDetectionPoint.TransformDirection(Vector2.right) * 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(grabObjectDetectionPoint.position, right, 1.5f);
        if (hit && !isGrabbingObject)
        {
            if (hit.transform.tag == "GrabbableObject")
            {
                grabbedObject = hit.transform.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                grabbedObject.transform.parent = this.transform;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                if (facingRight)
                {
                    grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position + new Vector2(1.65f, 0.35f);
                }
                else
                {
                    grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position + new Vector2(-1.65f, 0.35f);
                }
                grabbedObject.GetComponent<Rigidbody2D>().mass = 1;
                isGrabbingObject = true;
            }
        }
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

    public bool CanExit() {
        if ((inputs["Up"] || inputs["W"]) && lastDoorUseTime + 1f < Time.time)
        {
            lastDoorUseTime = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }
}
