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
        new OverlayDefinition(OverlayDefinition.Type.FogOfWar),
        new OverlayDefinition(OverlayDefinition.Type.Hover)
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
            OverlayDefinition def = Overlays[(int)OverlayDefinition.Type.FogOfWar];
            tile.Overlay.sprite = def.Sprite;
            tile.Overlay.color = def.Color;
        }

        if (WorldRepresentation.ContainsKey(new Vector2(0, 0)))
        {
            WorldMapHexagonTile tile = WorldRepresentation[new Vector2(0, 0)];
            tile.Visited = true;
            tile.Overlay.sprite = null;
        }

        UpdateFogOfWar();
    }

    public void UpdateFogOfWar()
    {
        foreach (WorldMapHexagonTile tile in WorldRepresentation.Values)
        {
            ClearTileFOW(tile);
        }
    }

    void ClearTileFOW(WorldMapHexagonTile tile)
    {
        if (tile.Visited)
        {
            for (int i = 0; i < tile.Neighbours.Length; i++)
            {
                if (tile.Neighbours[i] != null)
                {
                    tile.Neighbours[i].Overlay.sprite = null;
                }
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

    public void MapModeVisisted()
    {
        Debug.Log("MapMode: Visited Tiles");

        foreach (WorldMapHexagonTile tile in WorldRepresentation.Values)
        {
            if (tile.Visited)
            {
                tile.Text.text = "Visited";
                tile.Text.color = new Color().RGB32(0xFF, 0x00, 0x00);
            }
            else
            {
                tile.Text.text = "Not\nVisisted";
                tile.Text.color = new Color().RGB32(0x00, 0x00, 0xFF);
            }
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
    public enum Type { FogOfWar, Hover }

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