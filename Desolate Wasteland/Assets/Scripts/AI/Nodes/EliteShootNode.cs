using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EliteShootNode : Node
{

    private AI ai;
    private EliteEnemy enemy;

    public EliteShootNode(AI ai, EliteEnemy enemy)
    {
        this.ai = ai;
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        Transform closest = ai.GetClosestHero();
        GridManager.Instance.GetTileAtPosition(closest.position).OccupiedUnit.GetComponent<BaseHero>().takeDamage(EliteEnemy.GetDamage());
        enemy.ammo -= 1;
        return NodeState.SUCCESS;
    }

}