using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public bool locked;
    public int leadsToScene;

    StateComponent stateComponent;

    // Start is called before the first frame update
    void Start()
    {
        stateComponent = FindObjectOfType<StateComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor() {
        print("Door: OpenDoor");
        if (locked) {
            if (stateComponent.GetKeys() > 0) {
                SceneManager.LoadScene(leadsToScene);
                stateComponent.RemoveKey();
            }
        }
        else {
            SceneManager.LoadScene(leadsToScene);
        }
    }
}
