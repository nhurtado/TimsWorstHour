using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorController : MonoBehaviour
{
    [Header("General Settings:")]
    public bool locked;
    public int channel;
    public bool cantUse = false;
    [Range(1,2)]
    public int id = 1;

    GameObject player;
    GameObject otherDoor;
    public GameObject SonidoPuerta;
    StateComponent stateComponent;

    void Start()
    {
        player = GameObject.Find("Player");
        stateComponent = player.GetComponent<StateComponent>();

        GameObject[] otherDoors = GameObject.FindGameObjectsWithTag("Door");
        for (int i = 0; i < otherDoors.Length; i++) {
            if (otherDoors[i].GetComponent<DoorController>().channel == channel && otherDoors[i].GetComponent<DoorController>().id!= id) {
                otherDoor = otherDoors[i];
                break;
            }
        }
    }

    public void OpenDoor() {
        if (!cantUse)
        {
            if (locked)
            {
                if (stateComponent.GetKeys() > 0)
                {
                    Instantiate(SonidoPuerta);
                    stateComponent.RemoveKey();
                    locked = !locked;
                    player.transform.position = otherDoor.transform.position + new Vector3(0, 1);
                    stateComponent.iniPosition = player.transform.position;
                }
            }
            else
            {
                Instantiate(SonidoPuerta);
                player.transform.position = otherDoor.transform.position + new Vector3(0, 1);
                stateComponent.iniPosition = player.transform.position;
            }
        }
    }
}
