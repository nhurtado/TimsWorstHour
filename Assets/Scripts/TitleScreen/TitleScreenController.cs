using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Tower of Mages");
        }

        if (Event.current.Equals(Event.KeyboardEvent("escape")))
        {
            Application.Quit();
        }
    }
}
