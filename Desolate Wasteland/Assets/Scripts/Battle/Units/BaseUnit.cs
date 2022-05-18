using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile occupiedTile;
    public Faction faction;
    public string unitName;

    public static int initiative;

    private Pathfinding pathfinding;

    public int getInitiative()
    {
        return initiative;
    }

    public void Move(Tile targetTile)
    {
        pathfinding = new Pathfinding();
        List<Tile> path = pathfinding.FindPath(occupiedTile, targetTile);

        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                path[i].pathHighlight.SetActive(true);
            }
        }
    }
}