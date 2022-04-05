using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private MapTile walkableTile, terrainTile;

    [SerializeField] private SpriteRenderer map;

    private Dictionary<Vector2, MapTile> tiles;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        var mapFarCorner = map.transform.position + 0.5f * map.bounds.size;
        var mapOriginCorner = map.transform.position - 0.5f * map.bounds.size;
        tiles = new Dictionary<Vector2, MapTile>();
        for (int i = (int)(mapOriginCorner.x + 0.5f); i < mapFarCorner.x; i++)
        {
            for (int j = (int)(mapOriginCorner.y + 0.5f); j < mapFarCorner.y; j++)
            {
                Vector3 spawn = new Vector3(i, j);
                MapTile spawnedTile;
                Collider2D[] colliders = new Collider2D[2];
                int col = Physics2D.OverlapBoxNonAlloc(spawn, new Vector2(0.1f, 0.1f), 0, colliders);
                if (j + 1 >= mapFarCorner.y || i + 1 >= mapFarCorner.x || j == (int)(mapOriginCorner.y + 0.5f) || i == (int)(mapOriginCorner.x + 0.5f) || Random.Range(0, 10) <= 1 && col <= 1)
                {
                    spawnedTile = Instantiate(terrainTile, spawn, Quaternion.identity);
                }
                else
                {
                    spawnedTile = Instantiate(walkableTile, spawn, Quaternion.identity);
                }
                spawnedTile.name = $"Tile {i} {j}";
                spawnedTile.transform.parent = gameObject.transform;

                tiles[spawn] = spawnedTile;
            }
        }


    }

    public MapTile GetTileAtPos(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
}
