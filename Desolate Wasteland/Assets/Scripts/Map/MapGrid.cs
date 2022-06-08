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

    private Dictionary<Vector2, MapTile> tiles = new Dictionary<Vector2, MapTile>();
    public Dictionary<Vector2, GameObject> locations = new Dictionary<Vector2, GameObject>();

    private void Start()
    {
        mapFarCorner = map.transform.position + 0.5f * map.bounds.size;
        mapOriginCorner = map.transform.position - 0.5f * map.bounds.size;

        //GenerateLocations(5, 5, 5, 5, 20, 20, 20, 20);
        //GenerateGrid();

        if (SaveSerial.terrain == null)
        {
            Debug.Log("new");
            NewMap();

        }
        else
        {
            Debug.Log("load");
            Load();

        }


    }

    void NewMap()
    {
        GenerateLocations(5, 5, 5, 5, 20, 20, 20, 20);
        GenerateGrid();
        //SaveSerial.locations = Instantiate(locations);

        SaveSerial.terrain = new Dictionary<float[], string>();
        var xd = tiles.GetEnumerator();
        while (xd.MoveNext())
        {
            if (xd.Current.Value.CompareTag("Terrain"))
            {
                float x = xd.Current.Key.x;
                float y = xd.Current.Key.y;
                SaveSerial.terrain.Add(new float[] { x, y }, xd.Current.Value.name);
                //Debug.Log(xd.Current.Key);
            }
        }
        //Debug.Log(SaveSerial.locations.Count);


        //Piles & Locations
        SaveSerial.piles = new Dictionary<float[], string>();
        SaveSerial.locations = new Dictionary<float[], string>();
        SaveSerial.captured = new Dictionary<float[], bool>();

        var enumerator = locations.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Value.name.Equals("Metal") || enumerator.Current.Value.name.Equals("Electronics") || enumerator.Current.Value.name.Equals("Plastics") || enumerator.Current.Value.name.Equals("Food"))
            {
                float x = enumerator.Current.Key.x;
                float y = enumerator.Current.Key.y;
                SaveSerial.piles.Add(new float[] { x, y }, enumerator.Current.Value.name);
                //Debug.Log("Pile saved");
            }
            if (enumerator.Current.Value.name.Equals("Scrapyard"))
            {
                float x = enumerator.Current.Key.x;
                float y = enumerator.Current.Key.y;
                SaveSerial.locations.Add(new float[] { x, y }, enumerator.Current.Value.name);
                //Debug.Log("scrapyard saved");
            }
            if (enumerator.Current.Value.name.Equals("Shoping Center"))
            {
                float x = enumerator.Current.Key.x;
                float y = enumerator.Current.Key.y;
                SaveSerial.locations.Add(new float[] { x, y }, enumerator.Current.Value.name);
                //Debug.Log("center saved");
            }
            if (enumerator.Current.Value.name.Equals("Industrial Park"))
            {
                float x = enumerator.Current.Key.x;
                float y = enumerator.Current.Key.y;
                SaveSerial.locations.Add(new float[] { x, y }, enumerator.Current.Value.name);
                //Debug.Log("park saved");
            }
            if (enumerator.Current.Value.name.Equals("Hydrophonics"))
            {
                float x = enumerator.Current.Key.x;
                float y = enumerator.Current.Key.y;
                SaveSerial.locations.Add(new float[] { x, y }, enumerator.Current.Value.name);
                //Debug.Log("hydro saved");
            }
        }
    }

    public void Load()
    {
        GenerateCampFactory();
        var dict = SaveSerial.terrain;
        var enumerator = dict.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var ter = Instantiate(terrainTile, new Vector2(enumerator.Current.Key[0], enumerator.Current.Key[1]), Quaternion.identity);
            ter.name = enumerator.Current.Value;
            //Debug.Log(ter.name);
            tiles.Add(ter.transform.position, ter);
            ter.transform.parent = gameObject.transform;
        }

        var dictPiles = SaveSerial.piles;
        var enumeratorPiles = dictPiles.GetEnumerator();
        while (enumeratorPiles.MoveNext())
        {
            //GameObject g = Instantiate(pile, loc, Quaternion.identity);
            var pileLoad = Instantiate(pile, new Vector2(enumeratorPiles.Current.Key[0], enumeratorPiles.Current.Key[1]), Quaternion.identity);
            pileLoad.name = enumeratorPiles.Current.Value;
            pileLoad.transform.parent = gameObject.transform;
            //Debug.Log(pileLoad.name);
            locations.Add(pileLoad.transform.position, pileLoad);
        }

        var dictLoc = SaveSerial.locations;
        var enumeratorLocs = dictLoc.GetEnumerator();
        while (enumeratorLocs.MoveNext())
        {
            //GameObject g = Instantiate(pile, loc, Quaternion.identity);
            var x = enumeratorLocs.Current.Key[0];
            var y = enumeratorLocs.Current.Key[1];
            enumeratorLocs.MoveNext();
            x += enumeratorLocs.Current.Key[0];
            y += enumeratorLocs.Current.Key[1];
            enumeratorLocs.MoveNext();
            x += enumeratorLocs.Current.Key[0];
            y += enumeratorLocs.Current.Key[1];
            enumeratorLocs.MoveNext();
            x += enumeratorLocs.Current.Key[0];
            y += enumeratorLocs.Current.Key[1];
            var locLoad = Instantiate(location, new Vector2(x / 4, y / 4), Quaternion.identity);
            locLoad.name = enumeratorLocs.Current.Value;
            locLoad.transform.parent = gameObject.transform;
            Debug.Log(locLoad.name);
            locations.Add(locLoad.transform.position, locLoad);

        }


        GenerateGrid();
    }
    void GenerateGrid()
    {
        bool loaded = false;
        if (tiles.Count != 0)
        {
            loaded = true;
        }
        for (int i = (int)(mapOriginCorner.x + 0.5f); i < mapFarCorner.x; i++)
        {
            for (int j = (int)(mapOriginCorner.y + 0.5f); j < mapFarCorner.y; j++)
            {
                Vector3 spawn = new Vector3(i, j);
                if (!tiles.ContainsKey(new Vector2(i, j)))
                {
                    MapTile spawnedTile;
                    Collider2D[] colliders = new Collider2D[2];
                    int col = Physics2D.OverlapBoxNonAlloc(spawn, new Vector2(0.1f, 0.1f), 0, colliders);
                    if (!loaded && (j + 1 >= mapFarCorner.y || i + 1 >= mapFarCorner.x || j == (int)(mapOriginCorner.y + 0.5f) || i == (int)(mapOriginCorner.x + 0.5f) || Random.Range(0, 30) <= 1 && col <= 1))
                    {
                        //Debug.Log("XDD");
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


    }

    public void GenerateLocations(int scrapyards, int hydrophonics, int industrialParks, int shopingCenters, int metal, int electronics, int food, int plastics)
    {

        GenerateCampFactory();

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

    public void GenerateCampFactory()
    {
        GameObject g = Instantiate(location);

        g.transform.localScale = new Vector3(30, 30, 1);
        GameObject factory = Instantiate(g, new Vector2((mapFarCorner.x + mapOriginCorner.x) / 4, (mapFarCorner.y + mapOriginCorner.y) / 2), Quaternion.identity);
        factory.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.4f);
        factory.name = "Factory";
        factory.transform.parent = gameObject.transform;
        locations.Add(factory.transform.position, factory);

        g.transform.localScale = new Vector3(20, 20, 1);
        GameObject camp = Instantiate(g, new Vector2(mapOriginCorner.x + 8, mapOriginCorner.y + 8), Quaternion.identity);
        camp.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.4f);
        camp.name = "Camp";
        camp.transform.parent = gameObject.transform;
        locations.Add(camp.transform.position, camp);

        Destroy(g);

        locations.Add(new Vector2(camp.transform.position.x + 1, camp.transform.position.y), camp);
        locations.Add(new Vector2(camp.transform.position.x, camp.transform.position.y + 1), camp);
        locations.Add(new Vector2(camp.transform.position.x + 1, camp.transform.position.y + 1), camp);
        locations.Add(new Vector2(camp.transform.position.x - 1, camp.transform.position.y), camp);
        locations.Add(new Vector2(camp.transform.position.x, camp.transform.position.y - 1), camp);
        locations.Add(new Vector2(camp.transform.position.x - 1, camp.transform.position.y - 1), camp);
        locations.Add(new Vector2(camp.transform.position.x + 1, camp.transform.position.y - 1), camp);
        locations.Add(new Vector2(camp.transform.position.x - 1, camp.transform.position.y + 1), camp);

        for (int i = 1; i < 3; i++)
        {
            locations.Add(new Vector2(factory.transform.position.x + i, factory.transform.position.y), factory);
            locations.Add(new Vector2(factory.transform.position.x, factory.transform.position.y + i), factory);
            locations.Add(new Vector2(factory.transform.position.x - i, factory.transform.position.y), factory);
            locations.Add(new Vector2(factory.transform.position.x, factory.transform.position.y - i), factory);
            for (int j = 1; j < 3; j++)
            {
                locations.Add(new Vector2(factory.transform.position.x + i, factory.transform.position.y + j), factory);
                locations.Add(new Vector2(factory.transform.position.x - i, factory.transform.position.y - j), factory);
                locations.Add(new Vector2(factory.transform.position.x + i, factory.transform.position.y - j), factory);
                locations.Add(new Vector2(factory.transform.position.x - i, factory.transform.position.y + j), factory);
            }

        }
    }

    public GameObject GenerateLocation(string name)
    {
        int x = (int)Random.Range(mapOriginCorner.x + 2, mapFarCorner.x - 2);
        int y = (int)Random.Range(mapOriginCorner.y + 2, mapFarCorner.y - 2);
        while (true)
        {
            if (!locations.ContainsKey(new Vector2(x, y)) && !locations.ContainsKey(new Vector2(x + 1, y)) && !locations.ContainsKey(new Vector2(x + 1, y + 1)) && !locations.ContainsKey(new Vector2(x, y + 1)))
            {
                Vector2 loc = new Vector2(x, y);
                GameObject g = Instantiate(location, new Vector2(loc.x + 0.5f, loc.y + 0.5f), Quaternion.identity);
                g.name = name;
                locations.Add(loc, g);
                locations.Add(new Vector2(loc.x + 1, loc.y), g);
                locations.Add(new Vector2(loc.x + 1, loc.y + 1), g);
                locations.Add(new Vector2(loc.x, loc.y + 1), g);
                //Debug.Log(name + " coords: x=" + (loc.x + 1) + " " + loc.x + " y=" + (loc.y + 1) + " " + loc.y);
                g.transform.parent = gameObject.transform;
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
                g.name = name;
                locations.Add(g.transform.position, g);
                g.transform.parent = gameObject.transform;
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
