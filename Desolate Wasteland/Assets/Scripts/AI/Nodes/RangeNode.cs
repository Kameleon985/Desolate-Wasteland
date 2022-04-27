using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float range;
    private BaseUnit gObject;
    private EnemyAI ai;

    public RangeNode(float range, BaseUnit gObject, EnemyAI ai)
    {
        this.range = range;
        this.gObject = gObject;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Tile heroTile = GridManager.Instance.GetTileAtPosition(player.transform.position);
        Tile enemyTile = GridManager.Instance.GetTileAtPosition(gObject.transform.position);
        //Debug.Log("hero: " + heroTile + ", enemy: " + enemyTile);
        int distance = Pathfinding.CalculateDistance(heroTile, enemyTile);
        //foreach (ScriptableUnit u in UnitManager.Instance.GetUnits())
        //{
        //    int dist = Pathfinding.CalculateDistance(GridManager.Instance.GetTileAtPosition(u.unitPrefab.gameObject.transform.position), GridManager.Instance.GetTileAtPosition(gObject.transform.position));
        //    if (dist < distance)
        //    {
        //        distance = dist;
        //        ai.SetClosest(u.unitPrefab);
        //    }
        //}
        //Debug.Log(distance);

        return distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
