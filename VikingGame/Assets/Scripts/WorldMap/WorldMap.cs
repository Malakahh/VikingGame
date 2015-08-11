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
        UpdateFogOfWar();
    }

    void InitializeFogOfWar()
    {
        foreach (WorldMapHexagonTile tile in this.WorldRepresentation.Values)
        {
            OverlayDefinition def = Overlays[(int)OverlayDefinition.Type.FogOfWar];
            tile.Overlay.sprite = def.Sprite;
            tile.Overlay.color = def.Color;
        }
    }

    public void UpdateFogOfWar()
    {
        if (WorldRepresentation.ContainsKey(new Vector2(0,0)))
        {
            WorldMapHexagonTile tile = WorldRepresentation[new Vector2(0, 0)];
            tile.Visited = true;
            tile.Overlay.sprite = null;
            UpdateFogOfWarTile(tile);
        }
    }

    void UpdateFogOfWarTile(WorldMapHexagonTile tile)
    {
        if (tile.Visited)
        { 
            foreach (WorldMapHexagonTile neighbour in tile.Neighbours)
            {
                neighbour.Overlay.sprite = null;
                UpdateFogOfWarTile(neighbour);
            }
        }
    }

    public void MapModeTileCoord()
    {
        Debug.Log("MapMode: Tile Coordinates");

        foreach (WorldMapHexagonTile tile in WorldRepresentation.Values)
        {
            tile.Text.text = tile.TileCoordinate.ToString();
        }
    }
}

[System.Serializable]
public class TerrainDefinition
{
    public enum Type { Plains, Forest, Mountains }

    public string Text;
    public Sprite Sprite;
    public int Weight = 1;

    public TerrainDefinition(string text, Sprite sprite)
    {
        this.Text = text;
        this.Sprite = sprite;
    }

    public TerrainDefinition(string text)
        : this(text, null)
    { }

    public TerrainDefinition(Type text)
        : this(text.GetName())
    { }
}

[System.Serializable]
public class BuildingDefinition
{
    public enum Type { Tavern }

    public string Text;
    public Sprite Sprite;

    public BuildingDefinition(string text, Sprite sprite)
    {
        this.Text = text;
        this.Sprite = sprite;
    }

    public BuildingDefinition(string text)
        : this(text, null)
    { }

    public BuildingDefinition(Type text)
        : this(text.GetName())
    { }
}

[System.Serializable]
public class OverlayDefinition
{
    public enum Type { FogOfWar }

    public string Text;
    public Sprite Sprite;
    public Color Color = new Color().RGB32(0xff, 0xff, 0xff);

    public OverlayDefinition(string text, Sprite sprite)
    {
        this.Text = text;
        this.Sprite = sprite;
    }

    public OverlayDefinition(string text)
        : this(text, null)
    { }

    public OverlayDefinition(Type text)
        : this(text.GetName())
    { }
}