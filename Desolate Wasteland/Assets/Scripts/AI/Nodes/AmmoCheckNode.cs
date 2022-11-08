using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCheckNode : Node
{
    private EliteEnemy enemy;

    public AmmoCheckNode(EliteEnemy enemy)
    {
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("AMMO = " + enemy.ammo);
        if (enemy.ammo <= 0)
        {
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
