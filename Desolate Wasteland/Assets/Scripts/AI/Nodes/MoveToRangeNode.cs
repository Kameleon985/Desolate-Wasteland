using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRangeNode : Node
{
    private RangeEnemy enemy;
    private AI ai;

    public MoveToRangeNode(RangeEnemy enemy, AI ai)
    {
        this.enemy = enemy;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform closestHero = ai.GetClosestHero();
        Pathfinding pf = new Pathfinding();
        Tile start = GridManager.Instance.GetTileAtPosition(enemy.transform.position);
        Tile closest = GridManager.Instance.GetTileAtPosition(closestHero.position);
        List<Tile> path = pf.FindPath(start, closest);
        if (path != null)
        {
            GridManager.Instance.ClearAStarTiles();
            foreach (Tile t in path)
            {
                if (Pathfinding.CalculateDistance(t, closest) <= enemy.attackRange && t.OccupiedUnit == null)
                {
                    //Debug.Log("move to range");
                    enemy.Move(t);
                    //t.SetUnit(enemy);
                    //GridManager.Instance.ClearAStarTiles();
                    //BattleMenuMenager.instance.UpdateQueue();
                    return NodeState.SUCCESS;
                }
            }
        }
        return NodeState.FAILURE;
    }
}
