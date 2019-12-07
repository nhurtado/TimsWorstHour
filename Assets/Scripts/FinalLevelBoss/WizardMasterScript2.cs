using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMasterScript2 : MonoBehaviour
{
    public List<Transform> teleportPoints;
    public Vector2 currentTeleportPoint;
    public Vector2 nextTeleportPoint;
    public GameObject player;
    public bool facingRight = true;
    public Transform fireBallSpawn;
    public GameObject fireBallPrefab;
    public float lastFireBallTime;


    public Vector2 playerPosition;
    public bool playerEnteredDefenseRange;
    public StateComponent playerStateComponent;

    public bool fighting = true;
    public GameObject SpeechBubble;
    public GameObject exitTrapdoor;
    
    public Dictionary<string, bool> worldState = new Dictionary<string, bool>();
    Node decisionTree;

    public float healthPoints = 5;

    public Sprite regularMaster;

    void Start()
    {
        player = GameObject.Find("Player");
        exitTrapdoor =  GameObject.Find("ExitTrapdoor");
        playerStateComponent = player.GetComponent<StateComponent>();
        teleportPoints = new List<Transform>();
        GameObject[] teleportSpawns = GameObject.FindGameObjectsWithTag("BossTeleport");
        foreach (GameObject teleportSpawn in teleportSpawns)
        {
            teleportPoints.Add(teleportSpawn.transform);
        }
        worldState.Add("PlayerInDefenseRange", false);
        worldState.Add("PassedFireBallTimeLimit", false);
        worldState.Add("ZeroHealthPoints", false);
        currentTeleportPoint = teleportPoints[0].position;

        ActionNode dieNode = new ActionNode();
        dieNode.actionMethod = Die;

        ActionNode attackNode = new ActionNode();
        attackNode.actionMethod = Attack;

        ActionNode teleportNode = new ActionNode();
        teleportNode.actionMethod = Teleport;

        ActionNode idleNode = new ActionNode();
        idleNode.actionMethod = Idle;

        BinaryNode attackDecisionNode = new BinaryNode();
        attackDecisionNode.yesNode = attackNode;
        attackDecisionNode.noNode = idleNode;
        attackDecisionNode.decision = "PassedFireBallTimeLimit";

        BinaryNode teleportDecisionNode = new BinaryNode();
        teleportDecisionNode.yesNode = teleportNode;
        teleportDecisionNode.noNode = attackDecisionNode;
        teleportDecisionNode.decision = "PlayerInDefenseRange";

        BinaryNode dieDecisionNode = new BinaryNode();
        dieDecisionNode.yesNode = dieNode;
        dieDecisionNode.noNode = teleportDecisionNode;
        dieDecisionNode.decision = "ZeroHealthPoints";

        decisionTree = dieDecisionNode;
    }

    void FixedUpdate()
    {
        if (fighting)
        {
            CheckPlayerPosition();
            CheckAndUpdateFacingPosition();
            UpdateWorldState();
            decisionTree.Decide(worldState);
        }
    }

    void UpdateWorldState()
    {
        worldState["PlayerInDefenseRange"] = playerEnteredDefenseRange;
        if (lastFireBallTime + 1.25f < Time.time && !worldState["PassedFireBallTimeLimit"])
        {
            worldState["PassedFireBallTimeLimit"] = true;
        }
        else if (worldState["PassedFireBallTimeLimit"])
        {
            worldState["PassedFireBallTimeLimit"] = false;
        }
        if (healthPoints < 1)
        {
            worldState["ZeroHealthPoints"] = true;
        }

    }

    void CheckPlayerPosition()
    {
        playerPosition = player.transform.position;
    }

    void CheckAndUpdateFacingPosition()
    {
        if (playerPosition.x > transform.position.x && !facingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
        else if (playerPosition.x < transform.position.x && facingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
        }
    }

    void Attack()
    {
        lastFireBallTime = Time.time;
        GameObject go = Instantiate(fireBallPrefab, fireBallSpawn.position, fireBallSpawn.rotation);
        if (facingRight)
        {
            go.GetComponent<FireBallCollisionComponent2>().vec = (player.transform.position - fireBallSpawn.transform.position).normalized;
        }
        else
        {
            go.GetComponent<FireBallCollisionComponent2>().vec = new Vector2(-player.transform.position.x + fireBallSpawn.transform.position.x, player.transform.position.y - fireBallSpawn.transform.position.y).normalized;
        }
    }

    void Teleport()
    {
        nextTeleportPoint = teleportPoints[Random.Range(0, teleportPoints.Count)].position;
        while (nextTeleportPoint == currentTeleportPoint)
        {
            nextTeleportPoint = teleportPoints[Random.Range(0, teleportPoints.Count)].position;
        }
        transform.position = nextTeleportPoint;
        currentTeleportPoint = nextTeleportPoint;
        playerEnteredDefenseRange = false;
    }

    void Idle()
    {

    }

    void Die()
    {
        playerStateComponent.cantDie = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = regularMaster;
        fighting = false;
        gameObject.tag = "Player";
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        SpeechBubble.SetActive(true);
        SpeechBubble.GetComponent<Animator>().Play("SpeechFollowDemon");
        exitTrapdoor.SetActive(false);
    }
}
