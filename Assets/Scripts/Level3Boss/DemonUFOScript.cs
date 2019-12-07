using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonUFOScript : MonoBehaviour
{
    public GameObject player;
    public Transform fireBallSpawn;
    public GameObject fireBallPrefab;
    public float lastFireBallTime;
    public Vector2 playerPosition;
    public bool fighting = true;
    public int healthPoints = 3;
    public float speed = 2.5f;
    public float rightLimit = 5f;
    public float leftLimit = 5f;
    public bool canAttack = true;
    Rigidbody2D rb;
    Vector2 initialPosition;
    public GameObject urnPiece;

    public Dictionary<string, bool> worldState = new Dictionary<string, bool>();
    Node decisionTree;

    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        initialPosition = rb.position;

        FreezeWorldComponent.FreezeEvent += FreezeObject;
        FreezeWorldComponent.UnfreezeEvent += UnfreezeObject;

        worldState.Add("CanAttack", false);
        worldState.Add("ZeroHealthPoints", false);

        ActionNode attackNode = new ActionNode();
        attackNode.actionMethod = Attack;

        ActionNode moveNode = new ActionNode();
        moveNode.actionMethod = CheckAndUpdateMoveDirection;

        ActionNode dieNode = new ActionNode();
        dieNode.actionMethod = Die;

        BinaryNode attackDecisionNode = new BinaryNode();
        attackDecisionNode.yesNode = attackNode;
        attackDecisionNode.noNode = moveNode;
        attackDecisionNode.decision = "CanAttack";


        BinaryNode healthDecisionNode = new BinaryNode();
        healthDecisionNode.yesNode = dieNode;
        healthDecisionNode.noNode = attackDecisionNode;
        healthDecisionNode.decision = "ZeroHealthPoints";

        decisionTree = healthDecisionNode;

        rb.velocity = new Vector2(speed, 0);
    }

    void Update()
    {
        CheckHealthPoints();
        CheckLastAttack();
        decisionTree.Decide(worldState);
    }

    void CheckHealthPoints()
    {
        if (healthPoints < 1)
        {
            worldState["ZeroHealthPoints"] = true;
        }
    }

    void CheckLastAttack()
    {
        if (lastFireBallTime + 1f < Time.time && canAttack)
        {
            worldState["CanAttack"] = true;
        }
        else
        {
            worldState["CanAttack"] = false;
        }
    }

    void Attack()
    {
        lastFireBallTime = Time.time;
        GameObject go = Instantiate(fireBallPrefab, fireBallSpawn.position, fireBallSpawn.rotation);
        go.GetComponent<PlasmaBallCollisionComponent>().vec = (player.transform.position - fireBallSpawn.transform.position).normalized;
    }

    void CheckAndUpdateMoveDirection()
    {
        if (rb.position.x >= initialPosition.x + rightLimit)
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        else if (rb.position.x <= initialPosition.x - leftLimit)
        {
            rb.velocity = new Vector2(speed, 0);
        }
    }

    void Die()
    {
        player.transform.gameObject.GetComponent<StateComponent>().cantDie = true;
        urnPiece.SetActive(true);
        urnPiece.transform.parent = null;
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlasmaBall")
        {
            if (rb.position.x < initialPosition.x)
            {
                rb.velocity = new Vector2(-speed, 0);
                healthPoints = healthPoints - 1;
            }
            else
            {
                rb.velocity = new Vector2(speed, 0);
                healthPoints = healthPoints - 1;
            }

        }
    }

    void FreezeObject()
    {
        speed = 0;
        canAttack = false;
    }

    void UnfreezeObject()
    {
        speed = 2.5f;
        canAttack = true;
    }
}
