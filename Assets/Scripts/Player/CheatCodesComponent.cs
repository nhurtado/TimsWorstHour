using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodesComponent : MonoBehaviour
{
    //Next Level
    private string[] cheatCode1 = new string[] { "f", "a", "s", "t", "f", "o", "r", "w", "a", "r", "d" };
    private int cheatCode1Index = 0;

    //Can't Die
    private string[] cheatCode2 = new string[] { "i", "d", "d", "q", "d"};
    private int cheatCode2Index = 0;

    //Upgrade Timefreeze
    private string[] cheatCode3 = new string[] { "c", "h", "a", "o", "s", "c", "o", "n", "t", "r", "o", "l" };
    private int cheatCode3Index = 0;

    //Upgrade Timefreeze cooldown
    private string[] cheatCode4 = new string[] { "m", "r", "f", "r", "e", "e", "z", "e" };
    private int cheatCode4Index = 0;

    //Fly
    private string[] cheatCode5 = new string[] { "e", "t", "g", "o", "e", "s", "h", "o", "m", "e" };
    private int cheatCode5Index = 0;

    void Start()
    {

    }

    void Update()
    {
        cheatCode1Index = TryCheatCode(cheatCode1Index, cheatCode1, NextLevel);
        cheatCode2Index = TryCheatCode(cheatCode2Index, cheatCode2, Invincibility);
        cheatCode3Index = TryCheatCode(cheatCode3Index, cheatCode3, FreezeCheat);
        cheatCode4Index = TryCheatCode(cheatCode4Index, cheatCode4, CooldownCheat);
        cheatCode5Index = TryCheatCode(cheatCode5Index, cheatCode5, FlyCheat);
    }

    int TryCheatCode(int cheatCodeIndex, string[] cheatCode, Func<bool> cheatMethod)
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKey(cheatCode[cheatCodeIndex]))
            {
                cheatCodeIndex++;
            }
            else if (cheatCodeIndex > 0)
            {
                if (!Input.GetKey(cheatCode[cheatCodeIndex - 1]))
                {
                    cheatCodeIndex = 0;
                }
            }
        }
        if (cheatCodeIndex == cheatCode.Length)
        {
            cheatMethod();
            cheatCodeIndex = 0;
        }
        return cheatCodeIndex;
    }

    bool NextLevel()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetFloat("Time Freeze Limit", FreezeWorldComponent.instance.timeFreezeLimit);
        PlayerPrefs.SetFloat("Time Freeze Cooldown", FreezeWorldComponent.instance.timeFreezeCooldown);
        FreezeWorldComponent.instance.ResetWorld();
        EraChangeWorldComponent.instance.ResetWorld();
        if (SceneName == "Tower of Mages")
        {
            SceneManager.LoadScene("Jurassic Jungle");
        }
        else if (SceneName == "Jurassic Jungle")
        {
            SceneManager.LoadScene("Fantastic Future");
        }
        else
        {
            SceneManager.LoadScene("TitleScreen");
        }


        return true;
    }

    bool Invincibility()
    {
        transform.gameObject.GetComponent<StateComponent>().cantDie = true;
        return true;
    }

    bool FreezeCheat()
    {
        FreezeWorldComponent.instance.UpgradeFreeze();
        return true;
    }

    bool CooldownCheat()
    {
        FreezeWorldComponent.instance.UpgradeCooldown();
        return true;
    }

    bool FlyCheat()
    {
        transform.gameObject.GetComponent<StateComponent>().canFly= true;
        return true;
    }
}