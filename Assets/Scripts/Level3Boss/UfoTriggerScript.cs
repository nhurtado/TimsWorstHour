using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoTriggerScript : MonoBehaviour
{
    public GameObject Trigger;
    public GameObject DemonUFO;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DemonUFO.SetActive(true);
        }
    }
}
