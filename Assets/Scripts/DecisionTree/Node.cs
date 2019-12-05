using System;
using System.Collections.Generic;

public class Node
{
    public virtual void Decide(Dictionary<string, bool> worldState) { }
}

class BinaryNode : Node
{
    public Node yesNode;
    public Node noNode;
    public string decision;
    public override void Decide(Dictionary<string, bool> worldState)
    {
        if (worldState[decision])
        {
            yesNode.Decide(worldState);
        }
        else
        {
            noNode.Decide(worldState);
        }
    }
}

class ActionNode : Node
{
    public Action actionMethod;
    public override void Decide(Dictionary<string, bool> worldState)
    {
        actionMethod();
    }
}