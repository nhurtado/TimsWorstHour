using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;
    public float playerVelocityOffset = 1.5f;
    public float cameraSpeed = 5;
    public GameObject BlueFilter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        BlueFilter.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        BlueFilter.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 st = transform.position;
        Vector3 ed = player.transform.position + new Vector3(0, 2.5f, -10);
        transform.position = Vector3.Lerp(st, ed, Time.deltaTime * cameraSpeed);
    }
}
