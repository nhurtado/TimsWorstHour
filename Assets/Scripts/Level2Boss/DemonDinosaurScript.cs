using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDinosaurScript : MonoBehaviour
{
    public float speed = 0;
    public float initialSpeed = 0;
    public Animator anim;
    public GameObject player;
    public GameObject urnPiece;

    void Start()
    {
        player = GameObject.Find("Player");
        FreezeWorldComponent.FreezeEvent += FreezeObject;
        FreezeWorldComponent.UnfreezeEvent += UnfreezeObject;
    }


    void FixedUpdate()
    {
        transform.Translate(new Vector2(Time.deltaTime * speed, 0));
    }

    public void TriggerDinosaur()
    {
        anim.Play("DemonDinosaurRunning");
        StartCoroutine("KillTheDinosaur");
        initialSpeed = 5;
    }

    IEnumerator KillTheDinosaur()
    {
        yield return new WaitForSeconds(60);
        player.transform.gameObject.GetComponent<StateComponent>().cantDie = true;
        gameObject.GetComponent<Animator>().enabled = false;
        urnPiece.SetActive(true);
        urnPiece.transform.parent = null;
        gameObject.SetActive(false);
    }

    void FreezeObject()
    {
        anim.Play("DemonDinosaurFrozen");
        speed = 0;
    }

    void UnfreezeObject()
    {
        anim.Play("DemonDinosaurRunning");
        speed = initialSpeed;
    }
}
