using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButtonScript : MonoBehaviour
{
    public GameObject player;
    public bool facingRight = true;
    GameObject colGameObject;
    public Transform bulletSpawner;
    public GameObject bulletPrefab;
    public Transform target1;
    public Transform target2;
    public bool pressToActivate;
    void OnTriggerEnter2D(Collider2D collision)
    {
        colGameObject = collision.gameObject;
        if (colGameObject.tag == "IceBall")
        {
            target1.gameObject.SetActive(pressToActivate);
            target2.gameObject.SetActive(pressToActivate);
            this.gameObject.SetActive(false);
            this.Attack();
        }
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Attack()
    {
        GameObject go = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
        if (facingRight)
        {
            go.GetComponent<PlasmaBallScript>().vec = Vector2.up;
        }
        else
        {
            go.GetComponent<PlasmaBallScript>().vec = Vector2.down;
        }
    }
}
