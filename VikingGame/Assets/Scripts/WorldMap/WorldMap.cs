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

    //Key: TileCoord (as opposed to world coord); value: Tile 
    public Dictionary<Vector2, WorldMapHexagonTile> WorldRepresentation = new Dictionary<Vector2, WorldMapHexagonTile>();
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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