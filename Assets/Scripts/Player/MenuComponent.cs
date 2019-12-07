using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComponent : MonoBehaviour
{
    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("backspace")))
        {
            FreezeWorldComponent.instance.ResetWorld();
            EraChangeWorldComponent.instance.ResetWorld();
            SceneManager.LoadScene("TitleScreen");
        }

        if (Event.current.Equals(Event.KeyboardEvent("escape")))
        {
            Application.Quit();
        }
    }
}
