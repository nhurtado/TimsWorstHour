using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrnScript : MonoBehaviour
{
    public Animator anim;
    public bool playedOnce = false;
    void Start()
    {
        //anim.Play("UrnBreaking");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !playedOnce)
        {
            anim.Play("UrnBreaking");
        }
    }
}
