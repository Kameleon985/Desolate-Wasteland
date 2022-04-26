using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IsCoverAvaliableNode : Node
{
    private BaseUnit enemy;
    private EnemyAI ai;

    public IsCoverAvaliableNode(BaseUnit enemy, EnemyAI ai)
    {
        this.enemy = enemy;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform bestSpot = FindBestCoverSpot();
        ai.SetBestCoverSpot(bestSpot);
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

        //float minAngle = 90;
        //Transform bestSpot = null;
        //for (int i = 0; i < avaliableCovers.Length; i++)
        //{
        //    Transform bestSpotInCover = FindBestSpotInCover(avaliableCovers[i], ref minAngle);
        //    if (bestSpotInCover != null)
        //    {
        //        bestSpot = bestSpotInCover;
        //    }
        //}
        //return bestSpot;

        List<Vector2> terrain = GridManager.Instance.tiles.Where(t => t.Value.isWakable == false).OrderBy(t => Vector2.Distance(t.Key, enemy.transform.position)).Select(t => t.Key).ToList<Vector2>();

        foreach (Vector2 v in terrain)
        {
            //Debug.Log(v);
            Tile ti = GridManager.Instance.GetTileAtPosition(v);
            //Debug.Log(ti.name);
            List<Tile> spots = GetNeighbourList(v);
            foreach (Tile t in spots)
            {
                if (CheckIfSpotIsValid(t.transform))
                {
                    //Debug.Log("Best cover at: "+t.transform.position);
                    return t.transform;
                }
            }
        }

        return null;
    }

    //private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
    //{
    //    Transform[] avaliableSpots = cover.GetCoverSpots();
    //    Transform bestSpot = null;
    //    for (int i = 0; i < avaliableSpots.Length; i++)
    //    {
    //        Vector3 direction = enemy.transform.position - avaliableSpots[i].position;
    //        if (CheckIfSpotIsValid(avaliableSpots[i]))
    //        {
    //            float angle = Vector3.Angle(avaliableSpots[i].forward, direction);
    //            if (angle < minAngle)
    //            {
    //                minAngle = angle;
    //                bestSpot = avaliableSpots[i];
    //            }
    //        }
    //    }
    //    return bestSpot;
    //}

    private List<Tile> GetNeighbourList(Vector2 v)
    {
        List<Tile> neighbourList = new List<Tile>();
        Tile nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x - 1, v.y));
        if (nTile.x >= 0 && nTile.isWakable)
        {
            //Left
            neighbourList.Add(nTile);
            //Left Down
            nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x - 1, v.y - 1));
            if (nTile.y >= 0 && nTile.isWakable) neighbourList.Add(nTile);
            ////Left Up
            nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x - 1, v.y + 1));
            if (nTile.y < GridManager.Instance.height && nTile.isWakable) neighbourList.Add(nTile);
        }
        nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x + 1, v.y));
        if (nTile.x < GridManager.Instance.width && nTile.isWakable)
        {
            //Right
            neighbourList.Add(nTile);
            //Right Down
            nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x + 1, v.y - 1));
            if (nTile.y >= 0 && nTile.isWakable) neighbourList.Add(nTile);
            //Right Up
            nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x + 1, v.y + 1));
            if (nTile.y < GridManager.Instance.height && nTile.isWakable) neighbourList.Add(nTile);
        }
        //Down
        nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x, v.y - 1));
        if (nTile.y >= 0 && nTile.isWakable) neighbourList.Add(nTile);
        //UP
        nTile = GridManager.Instance.GetTileAtPosition(new Vector2(v.x, v.y + 1));
        if (nTile.y < GridManager.Instance.height && nTile.isWakable) neighbourList.Add(nTile);

        return neighbourList;
    }

    private bool CheckIfSpotIsValid(Transform spot)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit2D hit = Physics2D.Raycast(spot.position, player.transform.position - spot.position, Vector2.Distance(spot.position, player.transform.position), LayerMask.GetMask("Terrain"));
        if (hit)
        {
            if (hit.transform.CompareTag("Terrain"))
            {
                return true;
            }
        }
        return false;
    }
}