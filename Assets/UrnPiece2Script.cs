using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UrnPiece2Script : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().canMove = false;
            StartCoroutine("LoadNextLevelInSeconds");
        }
    }

    IEnumerator LoadNextLevelInSeconds()
    {
        PlayerPrefs.SetFloat("Time Freeze Limit", FreezeWorldComponent.instance.timeFreezeLimit);
        PlayerPrefs.SetFloat("Time Freeze Cooldown", FreezeWorldComponent.instance.timeFreezeCooldown);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3);
        FreezeWorldComponent.instance.ResetWorld();
        EraChangeWorldComponent.instance.ResetWorld();
        SceneManager.LoadScene("Tower of Ages");
    }
}
