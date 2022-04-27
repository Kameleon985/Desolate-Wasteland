using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverNode : Node
{
    private MeleeEnemy enemy;
    private EnemyAI ai;

    public GoToCoverNode(MeleeEnemy enemy, EnemyAI ai)
    {
        this.enemy = enemy;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform coverSpot = ai.GetBestCoverSpot();
        if (coverSpot == null)
            return NodeState.FAILURE;
        Tile tile = GridManager.Instance.GetTileAtPosition(coverSpot.position);
        enemy.Move(tile);
        tile.SetUnit(enemy);
        return NodeState.SUCCESS;
        //float distance = Vector3.Distance(coverSpot.position, agent.transform.position);
        //if (distance > 0.2f)
        //{
        //    agent.isStopped = false;
        //    agent.SetDestination(coverSpot.position);
        //    return NodeState.RUNNING;
        //}
        //else
        //{
        //    agent.isStopped = true;
        //    return NodeState.SUCCESS;
        //}
    }


}