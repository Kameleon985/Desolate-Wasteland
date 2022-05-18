using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IsCoverAvaliableNode : Node
{
    private BaseUnit enemy;
    private AI ai;

    public IsCoverAvaliableNode(BaseUnit enemy, AI ai)
    {
        this.enemy = enemy;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform bestSpot = FindBestCoverSpot();
        ai.SetBestCoverSpot(bestSpot);
        //Debug.Log(ai.GetBestCoverSpot());
        return bestSpot != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private Transform FindBestCoverSpot()
    {
        if (ai.GetBestCoverSpot() != null)
        {
            if (CheckIfSpotIsValid(ai.GetBestCoverSpot()))
            {
                return ai.GetBestCoverSpot();
            }
        }

        List<Vector2> terrain = GridManager.Instance.tiles.Where(t => t.Value.isWakable == false).OrderBy(t => Vector2.Distance(t.Key, enemy.transform.position)).Select(t => t.Key).ToList<Vector2>();

        foreach (Vector2 v in terrain)
        {
            //Debug.Log(v);
            Tile ti = GridManager.Instance.GetTileAtPosition(v);
            //Debug.Log(ti.name);
            List<Tile> spots = GetNeighbourList(v);
            foreach (Tile t in spots)
            {
                if (CheckIfSpotIsValid(t.transform) && t.OccupiedUnit == null)
                {
                    //Debug.Log("Best cover at: " + t.transform.position);
                    return t.transform;
                }
            }
        }

        return null;
    }


    public List<Tile> GetNeighbourList(Vector2 v)
    {
        List<Tile> neighbourList = new List<Tile>();
        Tile nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x - 1, v.y));
        if (nTile != null && nTile.x >= 0 && nTile.isWakable)
        {
            //Left
            neighbourList.Add(nTile);
            //Left Down
            nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x - 1, v.y - 1));
            if (nTile != null && nTile.y >= 0 && nTile.isWakable) neighbourList.Add(nTile);
            ////Left Up
            nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x - 1, v.y + 1));
            if (nTile != null && nTile.y < GridManager.Instance.height && nTile.isWakable) neighbourList.Add(nTile);
        }
        nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x + 1, v.y));
        if (nTile != null && nTile.x < GridManager.Instance.width && nTile.isWakable)
        {
            //Right
            neighbourList.Add(nTile);
            //Right Down
            nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x + 1, v.y - 1));
            if (nTile != null && nTile.y >= 0 && nTile.isWakable) neighbourList.Add(nTile);
            //Right Up
            nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x + 1, v.y + 1));
            if (nTile != null && nTile.y < GridManager.Instance.height && nTile.isWakable) neighbourList.Add(nTile);
        }
        //Down
        nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x, v.y - 1));
        if (nTile != null && nTile.y >= 0 && nTile.isWakable) neighbourList.Add(nTile);
        //UP
        nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x, v.y + 1));
        if (nTile != null && nTile.y < GridManager.Instance.height && nTile.isWakable) neighbourList.Add(nTile);

        return neighbourList;
    }

    private bool CheckIfSpotIsValid(Transform spot)
    {
        int i = 0;
        List<Transform> transforms = new List<Transform>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject g in players)
        {
            transforms.Add(g.transform);
        }
        foreach (Transform t in transforms)
        {
            RaycastHit2D hit = Physics2D.Raycast(spot.position, t.position - spot.position, Vector2.Distance(spot.position, t.position), LayerMask.GetMask("Terrain"));
            if (hit)
            {
                if (hit.transform.CompareTag("Terrain"))
                {
                    i += 1;
                }
            }
        }
        if (i == transforms.Count)
        {
            return true;
        }
        return false;
    }
}