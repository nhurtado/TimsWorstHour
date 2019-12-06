using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrnScript : MonoBehaviour
{
    public Animator anim;
    public bool playedOnce = false;
    public GameObject speechBubbleGrawlix;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !playedOnce)
        {
            playedOnce = true;
            anim.Play("UrnBreaking");
            collision.gameObject.GetComponent<PlayerScript>().canMove = false;
            StartCoroutine("LoadNextLevelInSeconds");
        }
    }

    IEnumerator LoadNextLevelInSeconds()
    {
        yield return new WaitForSeconds(8);
        speechBubbleGrawlix.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        speechBubbleGrawlix.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Load level");
    }
}
