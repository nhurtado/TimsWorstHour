using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;
    Rigidbody2D rb;
    public float playerVelocityOffset = 1.5f;
    public float cameraSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = player.transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 st = transform.position;
        Vector3 ed = player.transform.position + new Vector3(rb.velocity.x * playerVelocityOffset, 2.5f, -1);
        transform.position = Vector3.Lerp(st, ed, Time.deltaTime * cameraSpeed);
    }
}
