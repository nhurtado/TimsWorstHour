using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        PlayerPrefs.SetFloat("Time Freeze Limit", FreezeWorldComponent.instance.timeFreezeLimit);
        PlayerPrefs.SetFloat("Time Freeze Cooldown", FreezeWorldComponent.instance.timeFreezeCooldown);
        yield return new WaitForSeconds(5);
        speechBubbleGrawlix.SetActive(true);
        yield return new WaitForSeconds(2f);
        speechBubbleGrawlix.SetActive(false);
        yield return new WaitForSeconds(2f);
        FreezeWorldComponent.instance.ResetWorld();
        EraChangeWorldComponent.instance.ResetWorld();
        SceneManager.LoadScene("Jurassic Jungle");
    }
}
