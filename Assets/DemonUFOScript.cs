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
    Rigidbody2D rb;
    Vector2 initialPosition;

    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        initialPosition = rb.position;
        rb.velocity = new Vector2(speed, 0);
    }
    void Update()
    {
        this.Attack();
        this.MoveHorizontal();
        this.checkHealthPoints();
        this.MoveHorizontal();
    }

    void Attack()
    {
        lastFireBallTime = Time.time;
        GameObject go = Instantiate(fireBallPrefab, fireBallSpawn.position, fireBallSpawn.rotation);
        go.GetComponent<FireBallCollisionComponent>().vec = (player.transform.position - fireBallSpawn.transform.position).normalized;
    }
    void MoveHorizontal()
    {
        if (rb.position.x > initialPosition.x + rightLimit)
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        else if (rb.position.x < initialPosition.x - leftLimit)
        {
            rb.velocity = new Vector2(speed, 0);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlasmaBall"&& rb.position.x < initialPosition.x)
        {
            rb.velocity = new Vector2(-speed, 0);
            healthPoints = healthPoints - 1;
        }
        else if (collision.gameObject.tag == "PlasmaBall" && rb.position.x > initialPosition.x)
        {
            rb.velocity = new Vector2(speed, 0);
            healthPoints = healthPoints - 1;
        }

    }

    void checkHealthPoints()
    {
        if (healthPoints == 0)
        {
            Destroy(gameObject);
        }
    }

}
