using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float range;
    private BaseUnit gObject;
    private AI ai;

    public RangeNode(float range, BaseUnit gObject, AI ai)
    {
        this.range = range;
        this.gObject = gObject;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int distance = 1000;
        Transform closest = players[0].transform;
        foreach (GameObject g in players)
        {
            Tile heroTile = GridManager.Instance.GetTileAtPosition(g.transform.position);
            Tile enemyTile = GridManager.Instance.GetTileAtPosition(gObject.transform.position);
            int tmp = Pathfinding.CalculateDistance(heroTile, enemyTile);
            if (tmp < distance)
            {
                distance = tmp;
                closest = g.transform;
            }
        }
        ai.SetClosestHero(closest);
        return distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
