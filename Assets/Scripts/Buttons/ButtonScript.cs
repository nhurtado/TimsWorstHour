using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    GameObject colGameObject;
    public Transform target;
    void OnTriggerEnter2D(Collider2D collision)
    {
        colGameObject = collision.gameObject;
        if (colGameObject.tag == "Player" || colGameObject.tag == "IceBall" || colGameObject.tag == "GrabbableObject")
        {
            target.gameObject.SetActive(false);
        }
    }
}
