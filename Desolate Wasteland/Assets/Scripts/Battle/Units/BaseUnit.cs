using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    public Tile occupiedTile;
    public Faction faction;
    public string unitName;

    private Pathfinding pathfinding;

    public abstract int getInitiative();
    public abstract void setInitiative(int init);

    public void Move(Tile targetTile)
    {
        StartCoroutine(cor(targetTile));
    }

    private IEnumerator cor(Tile targetTile)
    {
        pathfinding = new Pathfinding();
        List<Tile> path = pathfinding.FindPath(occupiedTile, targetTile);


        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {

                path[i].pathHighlight.SetActive(true);
                //Debug.Log("Moveing");
                path[i].MoveUnit(this, new Vector3(path[i + 1].x, path[i + 1].y));
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

}