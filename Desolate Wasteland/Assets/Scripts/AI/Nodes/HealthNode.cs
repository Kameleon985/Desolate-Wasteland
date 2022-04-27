using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : Node
{
    private int health;
    private int threshold;

    public HealthNode(int health, int threshold)
    {
        this.health = health;
        this.threshold = threshold;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log(health <= threshold);
        return health <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
