using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodesComponent : MonoBehaviour
{
    private string[] cheatCode1 = new string[] { "i", "d", "k" };
    private int cheatCode1Index = 0;
    private string[] cheatCode2 = new string[] { "a", "s", "d" };
    private int cheatCode2Index = 0;

    void Start()
    {

    }

    void Update()
    {
        cheatCode1Index = TryCheatCode(cheatCode1Index, cheatCode1);
        cheatCode2Index = TryCheatCode(cheatCode2Index, cheatCode2);
    }

    int TryCheatCode(int cheatCodeIndex, string[] cheatCode)
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
            Debug.Log("Cheat1");
            cheatCodeIndex = 0;
        }
        return cheatCodeIndex;
    }
}