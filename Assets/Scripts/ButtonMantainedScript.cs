using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMantainedScript : MonoBehaviour
{
    GameObject colGameObject;
    public Transform target;
    float recentlyActivated;


    void OnTriggerStay2D(Collider2D collision)
    {
        colGameObject = collision.gameObject;
        if (colGameObject.tag == "Player" || colGameObject.tag == "IceBall" || colGameObject.tag == "GrabbableObject")
        {
            target.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        colGameObject = collision.gameObject;
        if (colGameObject.tag == "Player" || colGameObject.tag == "IceBall" || colGameObject.tag == "GrabbableObject")
        {
            target.gameObject.SetActive(true);
        }
    }
}
