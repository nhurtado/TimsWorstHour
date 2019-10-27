using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    public Tuple<float, Dictionary<string, bool>> GetInput(Dictionary<string, bool> keys)
    {
        float moving = Input.GetAxis("Horizontal");
        keys["R"] = Input.GetKey(KeyCode.R);
        keys["E"] = Input.GetKey(KeyCode.E);
        keys["F"] = Input.GetKey(KeyCode.F);
        keys["T"] = Input.GetKey(KeyCode.T);
        keys["Jump"] = Input.GetKey(KeyCode.Space);
        keys["Up"] = Input.GetKey(KeyCode.UpArrow);
        keys["W"] = Input.GetKey(KeyCode.W);
        return Tuple.Create(moving,keys);
    }
}
