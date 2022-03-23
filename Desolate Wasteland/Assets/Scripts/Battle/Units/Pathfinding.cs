using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public List<Tile> FindPath(Tile startTile, Tile targetTile)
    {
        var toSearch = new List<Tile>() { startTile };
        var searched = new List<Tile>();

        startTile.gCost = 0;
        startTile.hCost = CalculateDistance(startTile, targetTile);
        startTile.CalculateFCost();

        while(toSearch.Count > 0)
        {
            Tile currentTile = GetLowestFCostTile(toSearch);
            if (currentTile == targetTile)
            {
                //Reached target
                return CalcuatePath(targetTile);
            }

            toSearch.Remove(currentTile);
            searched.Add(currentTile);

            foreach (Tile neighbourTile in GetNeighbourList(currentTile))
            {
                if (searched.Contains(neighbourTile)) continue;

                int tentativeGCost = currentTile.gCost + CalculateDistance(currentTile, neighbourTile);
                if (tentativeGCost < neighbourTile.gCost)
                {
                    neighbourTile.previouseTile = currentTile;
                    neighbourTile.gCost = tentativeGCost;
                    neighbourTile.hCost = CalculateDistance(neighbourTile, targetTile);
                    neighbourTile.CalculateFCost();

                    if (!toSearch.Contains(neighbourTile))
                    {
                        toSearch.Add(neighbourTile);
                    }
                }
            }
        }

        return null;
    }

    private static List<Tile> GetNeighbourList(Tile currentTile)
    {
        List<Tile> neighbourList = new List<Tile>();

        if (currentTile.x - 1 >= 0)
        {
            //Left
            neighbourList.Add(GridManager.Instance.GetTileAtPosition(new Vector2(currentTile.x - 1, currentTile.y)));
            //Left Down
            if (currentTile.y - 1 >= 0) neighbourList.Add(GridManager.Instance.GetTileAtPosition(new Vector2(currentTile.x - 1, currentTile.y - 1)));
            ////Left Up
            if (currentTile.y + 1 <= GridManager.Instance.height) neighbourList.Add(GridManager.Instance.GetTileAtPosition(new Vector2(currentTile.x - 1, currentTile.y + 1)));
        }
        if (currentTile.x + 1 < GridManager.Instance.width)
        {
            //Right
            neighbourList.Add(GridManager.Instance.GetTileAtPosition(new Vector2(currentTile.x + 1, currentTile.y)));
            //Right Down
            if (currentTile.y - 1 >= 0) neighbourList.Add(GridManager.Instance.GetTileAtPosition(new Vector2(currentTile.x + 1, currentTile.y - 1)));
            //Right Up
            if (currentTile.y + 1 <= GridManager.Instance.height) neighbourList.Add(GridManager.Instance.GetTileAtPosition(new Vector2(currentTile.x + 1, currentTile.y + 1)));
        }
        //Down
        if (currentTile.y - 1 >= 0) neighbourList.Add(GridManager.Instance.GetTileAtPosition(new Vector2(currentTile.x, currentTile.y - 1)));
        //UP
        if (currentTile.y + 1 <= GridManager.Instance.height) neighbourList.Add(GridManager.Instance.GetTileAtPosition(new Vector2(currentTile.x, currentTile.y + 1)));

        return neighbourList;
    }

    private static List<Tile> CalcuatePath(Tile targetTile)
    {
        List<Tile> path = new List<Tile>();
        path.Add(targetTile);
        Tile currentTile = targetTile;
        while (currentTile.previouseTile != null)
        {
            path.Add(currentTile.previouseTile);
            currentTile = currentTile.previouseTile;
        }
        path.Reverse();
        return path;
    }


    private static int CalculateDistance(Tile a, Tile b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remainig = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remainig;
    }

    private static Tile GetLowestFCostTile(List<Tile> tileList)
    {
        Tile lowestFCostTile = tileList[0];
        for (int i = 1; i < tileList.Count; i++)
        {
            if(tileList[i].fCost < lowestFCostTile.fCost)
            {
                lowestFCostTile = tileList[i];
            }
        }

        return lowestFCostTile;
    }
}
