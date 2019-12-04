using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    Vector2 iniPosition;
    Rigidbody2D rb;
    public float respawnTime = 5f;
    public float speed = 1f;
    public bool goingRight = true;
    FreezeWorldComponent worldComponent;
    public int timeStopCounter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        iniPosition = rb.position;
        GameObject theWorld = GameObject.Find("TheWorld");
        worldComponent = theWorld.GetComponent<FreezeWorldComponent>();
        FreezeWorldComponent.FreezeEvent += AddToTimeStopCounter;
        StartCoroutine("RespawnRockAfterTime");
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (goingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "void")
        {
            GoToSpawn();
        }
    }

    void GoToSpawn()
    {
        StopCoroutine("RespawnRockAfterTime");
        timeStopCounter = 0;
        rb.velocity = new Vector2(0f, 0f);
        rb.position = iniPosition;
        StartCoroutine("RespawnRockAfterTime");
    }


    IEnumerator RespawnRockAfterTime()
    {
        yield return new WaitForSeconds(respawnTime);
        while (timeStopCounter > 0)
        {
            timeStopCounter -= 1;
            yield return new WaitForSeconds(worldComponent.timeFreezeLimit);
        }
        GoToSpawn();
    }

    void AddToTimeStopCounter()
    {
        timeStopCounter += 1;
    }
}
