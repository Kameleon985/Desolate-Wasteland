using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField] private MapTile walkableTile, terrainTile;

    [SerializeField] private SpriteRenderer map;

    [SerializeField] private GameObject location;

    [SerializeField] private GameObject pile;

    Vector3 mapFarCorner;
    Vector3 mapOriginCorner;

    private Dictionary<Vector2, MapTile> tiles;
    private Dictionary<Vector2, GameObject> locations = new Dictionary<Vector2, GameObject>();

    private void Start()
    {
        mapFarCorner = map.transform.position + 0.5f * map.bounds.size;
        mapOriginCorner = map.transform.position - 0.5f * map.bounds.size;
        GenerateLocations(2, 2, 2, 2, 2, 2, 2, 2);
        GenerateGrid();
    }

    void GenerateGrid()
    {

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

    public void GenerateLocations(int scrapyards, int hydrophonics, int industrialParks, int shopingCenters, int metal, int electronics, int food, int plastics)
    {
        GameObject factory = Instantiate(location, new Vector2((mapFarCorner.x + mapOriginCorner.x) / 4, (mapFarCorner.y + mapOriginCorner.y) / 2), Quaternion.identity);
        factory.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.4f);
        factory.transform.localScale = new Vector3(30, 30, 1);
        factory.name = "Factory";
        locations.Add(factory.transform.position, factory);
        for (int i = 1; i < 4; i++)
        {
            locations.Add(new Vector2(factory.transform.position.x + i, factory.transform.position.y + i), factory);
            locations.Add(new Vector2(factory.transform.position.x - i, factory.transform.position.y - i), factory);
        }

        for (int i = 0; i < scrapyards; i++)
        {
            GenerateLocation("Scrapyard");
        }
        for (int i = 0; i < hydrophonics; i++)
        {
            GenerateLocation("Hydrophonics");
        }
        for (int i = 0; i < industrialParks; i++)
        {
            GenerateLocation("Industrial Park");
        }
        for (int i = 0; i < shopingCenters; i++)
        {
            GenerateLocation("Shoping Center");
        }
        for (int i = 0; i < metal; i++)
        {
            GeneratePile("Metal");
        }
        for (int i = 0; i < electronics; i++)
        {
            GeneratePile("Electronics");
        }
        for (int i = 0; i < food; i++)
        {
            GeneratePile("Food");
        }
        for (int i = 0; i < plastics; i++)
        {
            GeneratePile("Plastics");
        }

    }

    public GameObject GenerateLocation(string name)
    {
        int x = (int)Random.Range(mapOriginCorner.x, mapFarCorner.x);
        int y = (int)Random.Range(mapOriginCorner.y, mapFarCorner.y);
        while (true)
        {
            if (!locations.ContainsKey(new Vector2(x, y)) && !locations.ContainsKey(new Vector2(x + 1, y)) && !locations.ContainsKey(new Vector2(x + 1, y + 1)) && !locations.ContainsKey(new Vector2(x, y + 1)))
            {
                Vector2 loc = new Vector2(x, y);
                GameObject g = Instantiate(location, new Vector2(loc.x + 0.5f, loc.y + 0.5f), Quaternion.identity);
                g.name = "Battle";
                locations.Add(loc, g);
                locations.Add(new Vector2(loc.x + 1, loc.y), g);
                locations.Add(new Vector2(loc.x + 1, loc.y + 1), g);
                locations.Add(new Vector2(loc.x, loc.y + 1), g);
                //Debug.Log(name + " coords: x=" + (loc.x + 1) + " " + loc.x + " y=" + (loc.y + 1) + " " + loc.y);
                return g;
            }
            else
            {
                x = (int)Random.Range(mapOriginCorner.x, mapFarCorner.x);
                y = (int)Random.Range(mapOriginCorner.y, mapFarCorner.y);
            }
        }
    }

    public GameObject GeneratePile(string name)
    {
        int x = (int)Random.Range(mapOriginCorner.x, mapFarCorner.x);
        int y = (int)Random.Range(mapOriginCorner.y, mapFarCorner.y);
        while (true)
        {
            Vector2 loc = new Vector2(x, y);
            if (!locations.ContainsKey(loc))
            {
                GameObject g = Instantiate(pile, loc, Quaternion.identity);
                g.name = "Battle";
                locations.Add(g.transform.position, g);
                return g;
            }
            else
            {
                x = (int)Random.Range(mapOriginCorner.x, mapFarCorner.x);
                y = (int)Random.Range(mapOriginCorner.y, mapFarCorner.y);
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
