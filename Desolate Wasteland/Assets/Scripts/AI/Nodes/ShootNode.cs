using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootNode : Node
{

    private AI ai;

    public ShootNode(AI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform closest = ai.GetClosestHero();
        GridManager.Instance.GetTileAtPosition(closest.position).OccupiedUnit.GetComponent<BaseHero>().takeDamage(RangeEnemy.GetDamage());
        return NodeState.SUCCESS;
    }

}