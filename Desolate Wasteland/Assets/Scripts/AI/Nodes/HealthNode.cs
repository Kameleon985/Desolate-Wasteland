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
        return health <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
