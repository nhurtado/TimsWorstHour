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
    public int id;

    GameObject player;
    GameObject otherDoor;
    StateComponent stateComponent;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor() {
        if (locked) {
            if (stateComponent.GetKeys() > 0) {
                player.transform.position = otherDoor.transform.position + new Vector3(0,1);
                stateComponent.RemoveKey();
            }
        }
        else {
            player.transform.position = otherDoor.transform.position + new Vector3(0, 1);
        }
    }
}
