using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtackNode : Node
{
    private AI ai;
    public MeleeAtackNode(AI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform hero = ai.GetClosestHero();
        GridManager.Instance.GetTileAtPosition(hero.position).OccupiedUnit.GetComponent<BaseHero>().takeDamage(MeleeEnemy.GetDamage());
        return NodeState.SUCCESS;
    }

}
