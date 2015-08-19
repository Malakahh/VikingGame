using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMapGen : MonoBehaviour {
    public static WorldMapGen Instance;

    public List<WorldMapTerrainDefinition> Terrain = new List<WorldMapTerrainDefinition>() {
        new WorldMapTerrainDefinition(WorldMapTerrainDefinition.Type.Plains),
        new WorldMapTerrainDefinition(WorldMapTerrainDefinition.Type.Forest),
        new WorldMapTerrainDefinition(WorldMapTerrainDefinition.Type.Mountains)
    };
    public List<BuildingDefinition> Buildings = new List<BuildingDefinition>() {
        new BuildingDefinition(BuildingDefinition.Type.Tavern)
    };
    public List<OverlayDefinition> Overlays = new List<OverlayDefinition>() {
        new OverlayDefinition(OverlayDefinition.Type.FogOfWar)
    };

    //Based on tile sizes
    float xOffset = 1f;
    float yOffset = 0.75f;


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
        if (DataCarrier.SelectedTileData == null)
        {
            GenerateHexagonTileGrid();
            DiscoverAllNeighbourTiles();
            MakeTerrain();
            MakeBuildings();
            GenerateAllDifficulty();
            InitializeFogOfWar();
        }
        else
        {
            RestoreHexagonTileGrid();
            DiscoverAllNeighbourTiles();
        }
    }

    void InitializeFogOfWar()
    {
        foreach (WorldMapHexagonTileData tileData in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            tileData.FogOfWar = true;
        }

        if (DataCarrier.PersistentData.WorldRepresentation.ContainsKey(new Vector2(0, 0)))
        {
            DataCarrier.PersistentData.WorldRepresentation[new Vector2(0, 0)].Visited = true;
        }
    }

    void GenerateAllDifficulty()
    {
        foreach (WorldMapHexagonTileData td in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            GenerateDifficulty(td);
        }
    }

    void GenerateDifficulty(WorldMapHexagonTileData tileData)
    {
        tileData.Difficulty = Vector2.Distance(tileData.TileCoordinate, Vector2.zero);
    }

    void RestoreHexagonTileGrid()
    {
        Dictionary<Vector2, WorldMapHexagonTileData> temp = new Dictionary<Vector2, WorldMapHexagonTileData>(DataCarrier.PersistentData.WorldRepresentation);
        DataCarrier.PersistentData.WorldRepresentation.Clear();

        foreach (Vector2 coord in temp.Keys)
        {
            WorldMapHexagonTile tile = ObjectPool.Instance.Acquire<WorldMapHexagonTile>();
            tile.TileData = temp[coord];
            DataCarrier.PersistentData.WorldRepresentation.Add(coord, tile.TileData);
            tile.transform.position = TileCoordToWorldCoord(tile.TileData.TileCoordinate);
            tile.gameObject.SetActive(true);
        }
    }

    void GenerateHexagonTileGrid()
    {
        for (int x = -(int)(mapSizeX / 2f - 0.5f); x < mapSizeX / 2f; x++)
        {
            for (int y = -(int)(mapSizeY / 2f - 0.5f); y < mapSizeY / 2f; y++)
            {
                WorldMapHexagonTile tile = ObjectPool.Instance.Acquire<WorldMapHexagonTile>();
                tile.transform.parent = WorldMap.Instance.transform;

                tile.TileData.TileCoordinate = new Vector2(x, y);
                tile.transform.position = TileCoordToWorldCoord(tile.TileData.TileCoordinate);
                DataCarrier.PersistentData.WorldRepresentation.Add(tile.TileData.TileCoordinate, tile.TileData);
                tile.gameObject.SetActive(true);
            }
        }
    }

    //Todo: Refactor this approach
    public Vector3 TileCoordToWorldCoord(Vector2 tileCoord)
    {
        return new Vector3(
            (tileCoord.y % 2 == 0) ? tileCoord.x * xOffset : tileCoord.x * xOffset + xOffset * 0.5f,
            tileCoord.y * yOffset,
            -0.5f);
    }

    void DiscoverAllNeighbourTiles()
    {
        foreach (WorldMapHexagonTileData tileData in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            DiscoverNeighbourTiles(tileData);
        }
    }

    //Todo: Refactor this approach
    void DiscoverNeighbourTiles(WorldMapHexagonTileData tileData)
    {
        for (int i = 0; i < tileData.Tile.Neighbours.Length; i++)
        {
            Vector2 neighbourCoord = new Vector2();
            if (tileData.TileCoordinate.y % 2 == 0)
            {
                switch (i)
                {
                    case 0:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x, tileData.TileCoordinate.y + 1);
                        break;
                    case 1:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x + 1, tileData.TileCoordinate.y);
                        break;
                    case 2:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x, tileData.TileCoordinate.y - 1);
                        break;
                    case 3:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x - 1, tileData.TileCoordinate.y - 1);
                        break;
                    case 4:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x - 1, tileData.TileCoordinate.y);
                        break;
                    case 5:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x - 1, tileData.TileCoordinate.y + 1);
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x + 1, tileData.TileCoordinate.y + 1);
                        break;
                    case 1:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x + 1, tileData.TileCoordinate.y);
                        break;
                    case 2:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x + 1, tileData.TileCoordinate.y - 1);
                        break;
                    case 3:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x, tileData.TileCoordinate.y - 1);
                        break;
                    case 4:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x - 1, tileData.TileCoordinate.y);
                        break;
                    case 5:
                        neighbourCoord = new Vector2(tileData.TileCoordinate.x, tileData.TileCoordinate.y + 1);
                        break;
                }
            }

            if (DataCarrier.PersistentData.WorldRepresentation.ContainsKey(neighbourCoord))
            {
                tileData.Tile.Neighbours[i] = DataCarrier.PersistentData.WorldRepresentation[neighbourCoord].Tile;
            }
        }
    }

    void MakeTerrain()
    {
        List<int> list = new List<int>();
        Terrain.ForEach(x => list.Add(x.Weight));

        WeightedRandomizer randomizer = new WeightedRandomizer(list);
        foreach (WorldMapHexagonTileData tileData in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            int ran = randomizer.GetRandomIndex();
            WorldMapTerrainDefinition def = Terrain[ran];
            tileData.Terrain = def;
        }
    }

    void MakeBuildings()
    {
        //Place tavern in center
        if (DataCarrier.PersistentData.WorldRepresentation.ContainsKey(new Vector2(0,0)))
        {
            WorldMapHexagonTileData tileData = DataCarrier.PersistentData.WorldRepresentation[new Vector2(0, 0)];
            tileData.Building = Buildings[(int)BuildingDefinition.Type.Tavern];
        }
    }
}

[System.Serializable]
public abstract class TerrainDefinition
{
    public enum Type { Plains, Forest, Mountains }
    
    public Type TerrainType;
    public string Text;
    public Sprite Sprite;
    public Color Mask = new Color().RGB32(0xFF, 0xFF, 0xFF);

    public TerrainDefinition(Type TerrainType, Sprite Sprite)
    {
        this.TerrainType = TerrainType;
        this.Sprite = Sprite;

        this.Text = this.TerrainType.GetName();
    }

    public TerrainDefinition(Type TerrainType)
        : this(TerrainType, null)
    { }
}

[System.Serializable]
public class WorldMapTerrainDefinition : TerrainDefinition
{
    public int Weight = 1;

    public WorldMapTerrainDefinition(Type TerrainType, Sprite Sprite) : base(TerrainType, Sprite)
    { }

    public WorldMapTerrainDefinition(Type TerrainType) : base(TerrainType)
    { }
}

[System.Serializable]
public class BuildingDefinition
{
    public enum Type { Tavern }

    public Type BuildingType;
    public string Text;
    public Sprite Sprite;
    public Color Mask = new Color().RGB32(0xff, 0xff, 0xff);

    public BuildingDefinition(Type BuildingType, Sprite sprite)
    {
        this.BuildingType = BuildingType;
        this.Sprite = sprite;

        this.Text = this.BuildingType.GetName();
    }

    public BuildingDefinition(Type BuildingType)
        : this(BuildingType, null)
    { }
}

[System.Serializable]
public struct OverlayDefinition
{
    public enum Type { FogOfWar }

    public Type OverlayType;
    public string Text;
    public Sprite Sprite;
    public Color Mask;

    public OverlayDefinition(Type OverlayType, Sprite sprite)
    {
        this.OverlayType = OverlayType;
        this.Sprite = sprite;

        this.Text = this.OverlayType.GetName();
        this.Mask = new Color().RGB32(0xff, 0xff, 0xff);
    }

    public OverlayDefinition(Type OverlayType)
        : this(OverlayType, null)
    { }
}