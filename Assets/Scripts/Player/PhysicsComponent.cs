using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
    public float jumpPower = 1;
    public float speed = 7;
    Dictionary<string, bool> inputs;

    public float jumpTime = 1;
    float jumpTimeCounter;
    bool isJumping = false;

    bool facingRight = true;
    bool facingWall = false;

    public bool isGrabbingObject = false;
    GameObject grabbedObject;
    
    public GameObject iceBallPrefab;
    public Transform iceBallSpawnerRight;
    public Transform iceBallSpawnerUp;
    public float iceBallLifeTime = 3.5f;
    GameObject iceBall;

    public Transform wallDetectionPoint;
    public Transform grabObjectDetectionPoint;

    Rigidbody2D rb;
    StateComponent stateComponent;
    FreezeWorldComponent worldComponent;

    float lastDoorUseTime = 0;

    RaycastHit2D hit;
    List<string> nonObstacles = new List<string>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateComponent = GetComponent<StateComponent>();
        stateComponent.iniPosition = rb.position;
        GameObject theWorld = GameObject.Find("TheWorld");
        worldComponent = theWorld.GetComponent<FreezeWorldComponent>();
        nonObstacles.Add("Orb");
        nonObstacles.Add("Key");
        nonObstacles.Add("Door");
        nonObstacles.Add("Enemy");
        nonObstacles.Add("Upper");
        nonObstacles.Add("Downer");
        nonObstacles.Add("Hazard");
        nonObstacles.Add("Platform");
        nonObstacles.Add("DialogueTrigger");
        nonObstacles.Add("GrabbableObject");
        nonObstacles.Add("Player");
        nonObstacles.Add("Button");
        nonObstacles.Add("IceBall");
        nonObstacles.Add("FireBall");
        nonObstacles.Add("Trigger");
        nonObstacles.Add("DefenseRange");
        nonObstacles.Add("Barrier");
        nonObstacles.Add("FireBallUpgrade");
        jumpTimeCounter = jumpTime;
    }

    void FixedUpdate()
    {
        CheckForObstacle();
    }

    void CheckForObstacle()
    {
        Vector3 right = wallDetectionPoint.TransformDirection(Vector2.right) * 0.1f;
        Vector3 up = wallDetectionPoint.TransformDirection(Vector2.up) * 0.1f;
        facingWall = false;
        for (int i = 0; i < 20; i++)
        {
            //Debug.DrawRay(wallDetectionPoint.position + up * i, right, Color.green);
            hit = Physics2D.Raycast(wallDetectionPoint.position + up * i, right, 0.05f);
            if (hit)
            {
                if (!nonObstacles.Contains(hit.transform.tag))
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
        if (inputs["Jump"])
        {
            if (stateComponent.canFly)
            {
                PlayerJump();
            }
            else if (stateComponent.isGrounded)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                PlayerJump();
            }
            else if (isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    PlayerJump();
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
        }
        else if (isJumping)
        {
            isJumping = false;
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
        if (inputs["Z"] && !worldComponent.isTimeFrozen)
        {
            if (inputs["W"])
            {
                IceBall(true);
            }
            else
            {
                IceBall();
            }
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
                moving = -0.05f;
            }
            else if (!facingRight && moving < 0)
            {
                moving = 0.05f;
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
                grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position + new Vector2(0, 2.25f);
                grabbedObject.GetComponent<Rigidbody2D>().mass = 1;
                isGrabbingObject = true;
            }
        }
    }

    public void ReleaseObject(bool throwObject = true)
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
            else
            {
                if (facingWall)
                {
                    grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position;
                }
                else if (facingRight)
                {
                    grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position + new Vector2(1.65f, 0.35f);
                }
                else
                {
                    grabbedObject.GetComponent<Rigidbody2D>().transform.position = (Vector2)transform.position + new Vector2(-1.65f, 0.35f);
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

    public void JumpByDamage(bool jumpRight, bool strong = false)
    {
        if (jumpRight)
        {
            if (strong)
            {
                rb.velocity = new Vector2(25, 10);
            }
            else
            {
                rb.velocity = new Vector2(3, 6);
            }
            
        }
        else
        {
            if (strong)
            {
                rb.velocity = new Vector2(-25, 10);
            }
            else
            {
                rb.velocity = new Vector2(-3, 6);
            }
        }
    }

    void IceBall(bool upwards = false)
    {
        if (iceBall == null)
        {
            GameObject newIceBall = iceBallPrefab;
            IceBallMovement iceBallMovement = newIceBall.GetComponent<IceBallMovement>();
            if (upwards && !isGrabbingObject)
            {
                iceBallMovement.direction = 0;
                iceBall = Instantiate(newIceBall, iceBallSpawnerUp.position, iceBallSpawnerUp.rotation);
            }
            else
            {
                if (facingRight)
                {
                    iceBallMovement.direction = 1;
                }
                else
                {
                    iceBallMovement.direction = 2;
                }
                iceBall = Instantiate(newIceBall, iceBallSpawnerRight.position, iceBallSpawnerRight.rotation);
            }
            StartCoroutine("DestroyIceBallWithTime");
        }  
    }

    IEnumerator DestroyIceBallWithTime()
    {
        float iceBallId = iceBall.GetInstanceID();
        yield return new WaitForSeconds(iceBallLifeTime);
        if (iceBall)
        {
            if (iceBallId == iceBall.GetInstanceID())
            {
                if (worldComponent.lastTimeFreeze + iceBallLifeTime > Time.time)
                {
                    yield return new WaitForSeconds(worldComponent.timeFreezeLimit);
                }
                if (iceBall)
                {
                    iceBall.GetComponent<IceBallFreezeComponent>().Unsubscribe();
                    Destroy(iceBall);
                }
            }
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
