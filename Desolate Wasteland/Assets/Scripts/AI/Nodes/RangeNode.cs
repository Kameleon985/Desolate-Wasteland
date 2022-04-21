using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float range;
    private BaseUnit gObject;

    public RangeNode(float range, BaseUnit gObject)
    {
        this.range = range;
        this.gObject = gObject;
    }

    public override NodeState Evaluate()
    {
        int distance = Pathfinding.CalculateDistance(GridManager.Instance.GetTileAtPosition(UnitManager.Instance.GetUnits()[0].unitPrefab.gameObject.transform.position), GridManager.Instance.GetTileAtPosition(gObject.transform.position));
        foreach (ScriptableUnit u in UnitManager.Instance.GetUnits())
        {
            int dist = Pathfinding.CalculateDistance(GridManager.Instance.GetTileAtPosition(u.unitPrefab.gameObject.transform.position), GridManager.Instance.GetTileAtPosition(gObject.transform.position));
            if (dist < distance)
            {
                distance = dist;
            }
        }
        return distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
