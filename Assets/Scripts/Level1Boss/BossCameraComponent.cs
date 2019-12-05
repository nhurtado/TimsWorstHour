using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraComponent : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject bossCamera;
    public bool togglesBossCamera = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (togglesBossCamera && mainCamera.activeSelf)
            {
                mainCamera.SetActive(false);
                bossCamera.SetActive(true);
            }
            else if (!togglesBossCamera && bossCamera.activeSelf)
            {
                bossCamera.SetActive(false);
                mainCamera.SetActive(true);
            }
        }
    }
}
