using UnityEngine;
using System.Collections.Generic;

public class WorldMap : MonoBehaviour {
    public static WorldMap Instance;

    public List<TerrainDefinition> Terrain = new List<TerrainDefinition>() {
        new TerrainDefinition(TerrainDefinition.Type.Plains),
        new TerrainDefinition(TerrainDefinition.Type.Forest),
        new TerrainDefinition(TerrainDefinition.Type.Mountains)
    };
    public List<BuildingDefinition> Buildings = new List<BuildingDefinition>() {
        new BuildingDefinition(BuildingDefinition.Type.Tavern)
    };
    public List<OverlayDefinition> Overlays = new List<OverlayDefinition>() {
        new OverlayDefinition(OverlayDefinition.Type.FogOfWar)
    };

    //Key: TileCoord (as opposed to world coord); value: Tile 
    public Dictionary<Vector2, WorldMapHexagonTile> WorldRepresentation = new Dictionary<Vector2, WorldMapHexagonTile>();
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        WorldMapGen.Instance.GenerateMap();
        InitializeFogOfWar();
    }

    void InitializeFogOfWar()
    {
        foreach (WorldMapHexagonTile tile in this.WorldRepresentation.Values)
        {
            tile.FogOfWar = true;
        }

        if (WorldRepresentation.ContainsKey(new Vector2(0, 0)))
        {
            WorldRepresentation[new Vector2(0, 0)].Visited = true;
        }
    }

    public void MapModeDefault()
    {
        Debug.Log("MapMode: Default");

        foreach (WorldMapHexagonTile tile in WorldRepresentation.Values)
        {
            tile.Terrain = tile.Terrain;
            tile.Building = tile.Building;
        }
    }

    public void MapModeTileCoord()
    {
        Debug.Log("MapMode: Tile Coordinates");

        foreach (WorldMapHexagonTile tile in WorldRepresentation.Values)
        {
            tile.Text.text = tile.TileCoordinate.ToString();
            tile.Sprite.color = tile.Terrain.Mask;
        }
    }

    public void MapModeVisisted()
    {
        Debug.Log("MapMode: Visited Tiles");

        foreach (WorldMapHexagonTile tile in WorldRepresentation.Values)
        {
            if (tile.Visited)
            {
                tile.Text.text = "Visited";
                tile.Sprite.color = new Color().RGB32(0xAA, 0xAA, 0xAA);
            }
            else
            {
                tile.Text.text = "Not\nVisisted";
                tile.Sprite.color = new Color().RGB32(0x00, 0xFF, 0x00);
            }
        }
    }
}

[System.Serializable]
public class TerrainDefinition
{
    public enum Type { Plains, Forest, Mountains }

    public Type TerrainType;
    public string Text;
    public Sprite Sprite;
    public int Weight = 1;
    public Color Mask = new Color().RGB32(0xff, 0xff, 0xff);

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
public class OverlayDefinition
{
    public enum Type { FogOfWar }

    public Type OverlayType;
    public string Text;
    public Sprite Sprite;
    public Color Mask = new Color().RGB32(0xff, 0xff, 0xff);

    public OverlayDefinition(Type OverlayType, Sprite sprite)
    {
        this.OverlayType = OverlayType;
        this.Sprite = sprite;

        this.Text = this.OverlayType.GetName();
    }

    public OverlayDefinition(Type OverlayType)
        : this(OverlayType, null)
    { }
}