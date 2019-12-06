using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool facingRight = true;
    public float moving;
    public bool canMove = true;
    Dictionary<string, bool> inputs;

    InputComponent inputComponent;
    GraphicsComponent graphicsComponent;
    PhysicsComponent physicsComponent;


    void Start()
    {
        inputs = new Dictionary<string, bool>();
        inputs.Add("R", false); //Throw Object
        inputs.Add("E", false); //Grab Object
        inputs.Add("F", false); //Put Object
        inputs.Add("T", false); //Stop Time
        inputs.Add("Jump", false);
        inputs.Add("Up", false); //Enter door
        inputs.Add("Z", false); //Throw snowball
        inputs.Add("C", false); //Era change
        inputComponent = GetComponent<InputComponent>();
        graphicsComponent = GetComponent<GraphicsComponent>();
        physicsComponent = GetComponent<PhysicsComponent>();
    }

    void FixedUpdate()
    {

        Tuple<float, Dictionary<string, bool>> inputResult = inputComponent.GetInput(inputs);
        moving = inputResult.Item1;
        inputs = inputResult.Item2;
        if (canMove)
        {
            physicsComponent.ProcessPlayerInputs(moving, inputs, facingRight);
            facingRight = graphicsComponent.UpdateSprite(facingRight, moving);
        }
    }
}

