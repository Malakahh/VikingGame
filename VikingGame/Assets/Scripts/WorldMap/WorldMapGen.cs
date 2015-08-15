using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMapGen : MonoBehaviour {
    public static WorldMapGen Instance;

    float magicXOffset = 0.65f;
    float magixYOffset = 0.49f;
    int mapSizeX = 19;
    int mapSizeY = 19;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GenerateMap()
    {
        if (DataCarrier.PersistentData.WorldRepresentation.Count == 0)
        {
            GenerateHexagonTileGrid();
            DiscoverAllNeighbourTiles();
            MakeTerrain();
            MakeBuildings();
        }
        else
        {
            RestoreHexagonTileGrid();
        }
    }

    void RestoreHexagonTileGrid()
    {
        foreach (WorldMapHexagonTile t in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            t.transform.position = TileCoordToWorldCoord(t.TileCoordinate);
            DiscoverNeighbourTiles(t);
        }
    }

    void GenerateHexagonTileGrid()
    {
        for (int x = -(int)(mapSizeX / 2f - 0.5f); x < mapSizeX / 2f; x++)
        {
            for (int y = -(int)(mapSizeY / 2f - 0.5f); y < mapSizeY / 2f; y++)
            {
                WorldMapHexagonTile tile = ObjectPool.Acquire<WorldMapHexagonTile>();
                //tile.transform.parent = WorldMap.Instance.transform;

                tile.TileCoordinate = new Vector2(x, y);
                tile.transform.position = TileCoordToWorldCoord(tile.TileCoordinate);
                DataCarrier.PersistentData.WorldRepresentation.Add(tile.TileCoordinate, tile);
                tile.gameObject.SetActive(true);
            }
        }
    }

    //Todo: Refactor this approach
    public Vector3 TileCoordToWorldCoord(Vector2 tileCoord)
    {
        return new Vector3(
            (tileCoord.y % 2 == 0) ? tileCoord.x * magicXOffset : tileCoord.x * magicXOffset + magicXOffset * 0.5f,
            tileCoord.y * magixYOffset,
            -0.5f);
    }

    void DiscoverAllNeighbourTiles()
    {
        foreach (WorldMapHexagonTile tile in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            DiscoverNeighbourTiles(tile);
        }
    }

    //Todo: Refactor this approach
    void DiscoverNeighbourTiles(WorldMapHexagonTile tile)
    {
        for (int i = 0; i < tile.Neighbours.Length; i++)
        {
            Vector2 neighbourCoord = new Vector2();
            if (tile.TileCoordinate.y % 2 == 0)
            {
                switch (i)
                {
                    case 0:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x, tile.TileCoordinate.y + 1);
                        break;
                    case 1:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x + 1, tile.TileCoordinate.y);
                        break;
                    case 2:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x, tile.TileCoordinate.y - 1);
                        break;
                    case 3:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x - 1, tile.TileCoordinate.y - 1);
                        break;
                    case 4:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x - 1, tile.TileCoordinate.y);
                        break;
                    case 5:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x - 1, tile.TileCoordinate.y + 1);
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x + 1, tile.TileCoordinate.y + 1);
                        break;
                    case 1:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x + 1, tile.TileCoordinate.y);
                        break;
                    case 2:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x + 1, tile.TileCoordinate.y - 1);
                        break;
                    case 3:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x, tile.TileCoordinate.y - 1);
                        break;
                    case 4:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x - 1, tile.TileCoordinate.y);
                        break;
                    case 5:
                        neighbourCoord = new Vector2(tile.TileCoordinate.x, tile.TileCoordinate.y + 1);
                        break;
                }
            }

            if (DataCarrier.PersistentData.WorldRepresentation.ContainsKey(neighbourCoord))
            {
                tile.Neighbours[i] = DataCarrier.PersistentData.WorldRepresentation[neighbourCoord];
            }
        }
    }

    void MakeTerrain()
    {
        List<int> list = new List<int>();
        WorldMap.Instance.Terrain.ForEach(x => list.Add(x.Weight));

        WeightedRandomizer randomizer = new WeightedRandomizer(list);
        foreach (WorldMapHexagonTile tile in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            int ran = randomizer.GetRandomIndex();
            TerrainDefinition def = WorldMap.Instance.Terrain[ran];
            tile.Terrain = def;
        }
    }

    void MakeBuildings()
    {
        //Place tavern in center
        if (DataCarrier.PersistentData.WorldRepresentation.ContainsKey(new Vector2(0,0)))
        {
            WorldMapHexagonTile tile = DataCarrier.PersistentData.WorldRepresentation[new Vector2(0, 0)];
            tile.Building = WorldMap.Instance.Buildings[(int)BuildingDefinition.Type.Tavern];
        }
    }
}
