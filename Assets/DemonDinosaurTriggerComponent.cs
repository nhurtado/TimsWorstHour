using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDinosaurTriggerComponent : MonoBehaviour
{
    public GameObject DemonDinosaur;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DemonDinosaur.GetComponent<DemonDinosaurScript>().speed = 5;
            DemonDinosaur.GetComponent<DemonDinosaurScript>().TriggerDinosaur();
        }
    }
}
