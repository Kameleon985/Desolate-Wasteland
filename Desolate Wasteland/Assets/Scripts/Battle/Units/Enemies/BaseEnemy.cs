using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseUnit
{

    private Pathfinding pathfinding;
    public int quantity = 1;



    public void Chase(Tile target)
    {
        pathfinding = new Pathfinding();
        List<Tile> path = pathfinding.FindPath(occupiedTile, target);

        if (path != null)
        {
            GridManager.Instance.ClearAStarTiles();
            Move(path[path.Count - 2]);
            //path[path.Count - 2].SetUnit(this);
            //GridManager.Instance.ClearAStarTiles();

        }
    }

    public abstract void takeDamage(int dmg);

}
