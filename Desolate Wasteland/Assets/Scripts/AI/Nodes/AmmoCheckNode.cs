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
        //Debug.Log("ammo: " + enemy.getAmmo());
        if (enemy.getAmmo() <= 0)
        {
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
