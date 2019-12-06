using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnComponent : MonoBehaviour
{
    public GameObject wizardMasterPrefab;
    public GameObject entranceTrapdoor;
    public GameObject exitTrapdoor;
    public Transform spawnPoint;
    public bool triggered = false;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;
            entranceTrapdoor.SetActive(true);
            Instantiate(wizardMasterPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void UnlockExit()
    {
        exitTrapdoor.SetActive(false);
    }
}
