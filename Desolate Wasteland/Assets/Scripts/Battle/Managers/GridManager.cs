using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public int width;
    public int height;

    public Tile grassTile;
    public Tile mountainTile;
    public GameObject notClickableThrough;
    public Transform cam;

    public Dictionary<Vector2, Tile> tiles;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x < 2 || x > width - 3)
                {
                    var spawnedTile = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";

                    spawnedTile.init(x, y, notClickableThrough);

                    tiles[new Vector2(x, y)] = spawnedTile;
                }
                else
                {
                    var randomTile = Random.Range(0, 6) == 3 ? mountainTile : grassTile;
                    var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";

                    spawnedTile.init(x, y, notClickableThrough);

                    tiles[new Vector2(x, y)] = spawnedTile;
                }

            }
        }

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);

        BattleMenager.instance.ChangeState(GameState.SpawnHeroes);
    }

    public Tile GetHeroSpawn()
    {
        return tiles.Where(t => t.Key.x < 2 && t.Value.isWalkableFinal).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawn()
    {
        return tiles.Where(t => t.Key.x > width - 3 && t.Value.isWalkableFinal).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    public void ClearAStarTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GetTileAtPosition(new Vector2(x, y)).previouseTile = null;
                GetTileAtPosition(new Vector2(x, y)).gCost = int.MaxValue;
                GetTileAtPosition(new Vector2(x, y)).CalculateFCost();
            }
        }
    }

    public void ClearAllHighlightTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //GetTileAtPosition(new Vector2(x, y)).pathHighlight.SetActive(false);
                GetTileAtPosition(new Vector2(x, y)).rangeHighlight.SetActive(false);
            }
        }
    }
}
