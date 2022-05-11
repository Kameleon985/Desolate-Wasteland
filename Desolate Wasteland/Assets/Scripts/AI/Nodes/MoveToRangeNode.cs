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
            foreach (Tile t in path)
            {
                if (Pathfinding.CalculateDistance(t, closest) <= enemy.attackRange)
                {
                    enemy.Move(t);
                    t.SetUnit(enemy);
                    GridManager.Instance.ClearAStarTiles();
                    return NodeState.SUCCESS;
                }
            }
        }
        return NodeState.FAILURE;
    }
}
