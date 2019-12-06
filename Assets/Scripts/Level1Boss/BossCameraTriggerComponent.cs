using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraTriggerComponent : MonoBehaviour
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
                mainCamera.transform.position = collision.transform.position;
                mainCamera.SetActive(true);
            }
        }
    }
}
