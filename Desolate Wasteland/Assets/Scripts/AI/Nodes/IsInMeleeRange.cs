using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInMeleeRange : Node
{
    private BaseUnit enemy;
    private AI ai;

    public IsInMeleeRange(BaseUnit enemy, AI ai)
    {
        this.ai = ai;
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        Tile t = (GridManager.Instance.GetTileAtPosition(enemy.transform.position));
        BaseUnit b = null;
        if (t.IsNeighborOccupied(GridManager.Instance.GetTileAtPosition(enemy.transform.position)))
        {
            IsCoverAvaliableNode e = new IsCoverAvaliableNode(enemy, ai);
            List<Tile> l = e.GetNeighbourList(t.transform.position);
            foreach (Tile tile in l)
            {
                if (tile.OccupiedUnit != null && tile.OccupiedUnit.faction == Faction.Hero)
                {
                    b = tile.OccupiedUnit;
                }
            }
        }
        if (b == null || b.faction != Faction.Hero)
        {
            return NodeState.FAILURE;
        }
        ai.SetClosestHero(b.transform);
        return NodeState.SUCCESS;
    }
}
