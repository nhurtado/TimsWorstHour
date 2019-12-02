using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [Header("General Settings:")]
    public bool locked;
    public int channel;
    [Range(1,2)]
    public int id = 1;

    GameObject player;
    GameObject otherDoor;
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
        if (locked) {
            if (stateComponent.GetKeys() > 0) {
                locked = !locked;
                stateComponent.RemoveKey();
            }
        }
        else {
            player.transform.position = otherDoor.transform.position + new Vector3(0, 1);
            stateComponent.iniPosition = player.transform.position;
        }
    }
}
