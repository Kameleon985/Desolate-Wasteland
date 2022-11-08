using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverNode : Node
{
    private BaseUnit enemy;
    private AI ai;

    public GoToCoverNode(BaseUnit enemy, AI ai)
    {
        this.enemy = enemy;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform coverSpot = ai.GetBestCoverSpot();
        //Debug.Log(coverSpot);
        if (coverSpot == null)
        {
            return NodeState.FAILURE;
        }
        Tile tile = GridManager.Instance.GetTileAtPosition(coverSpot.position);
        GridManager.Instance.ClearAStarTiles();
        enemy.Move(tile);
        //tile.SetUnit(enemy);
        //BattleMenuMenager.instance.UpdateQueue();
        //GridManager.Instance.ClearAStarTiles();
        return NodeState.SUCCESS;

    }


}