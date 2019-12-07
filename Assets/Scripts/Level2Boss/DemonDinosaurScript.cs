using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDinosaurScript : MonoBehaviour
{
    public float speed = 0;
    public float initialSpeed = 0;
    public Animator anim;
    public GameObject player;
    public GameObject urnPiece;
    public float triggeredTime;
    public bool triggered;

    public Dictionary<string, bool> worldState = new Dictionary<string, bool>();
    Node decisionTree;

    void Start()
    {
        player = GameObject.Find("Player");
        FreezeWorldComponent.FreezeEvent += FreezeObject;
        FreezeWorldComponent.UnfreezeEvent += UnfreezeObject;

        worldState.Add("OneMinuteHasPassed", false);

        ActionNode runNode = new ActionNode();
        runNode.actionMethod = DRun;

        ActionNode dieNode = new ActionNode();
        dieNode.actionMethod = Die;

        BinaryNode minuteDecisionNode = new BinaryNode();
        minuteDecisionNode.yesNode = dieNode;
        minuteDecisionNode.noNode = runNode;
        minuteDecisionNode.decision = "OneMinuteHasPassed";

        decisionTree = minuteDecisionNode;
    }


    void FixedUpdate()
    {
        Check60SecondsPassed();
        decisionTree.Decide(worldState);
    }

    public void TriggerDinosaur()
    {
        anim.Play("DemonDinosaurRunning");
        initialSpeed = 5;
        triggered = true;
        triggeredTime = Time.time;
    }

    void Check60SecondsPassed()
    {
        if (triggeredTime + 5 < Time.time && triggered)
        {
            worldState["OneMinuteHasPassed"] = true;
        }
    }

    void DRun()
    {
        transform.Translate(new Vector2(Time.deltaTime * speed, 0));
    }

    void Die()
    {
        player.transform.gameObject.GetComponent<StateComponent>().cantDie = true;
        gameObject.GetComponent<Animator>().enabled = false;
        urnPiece.SetActive(true);
        urnPiece.transform.parent = null;
        gameObject.SetActive(false);
    }

    void FreezeObject()
    {
        anim.Play("DemonDinosaurFrozen");
        speed = 0;
    }

    void UnfreezeObject()
    {
        anim.Play("DemonDinosaurRunning");
        speed = initialSpeed;
    }
}
