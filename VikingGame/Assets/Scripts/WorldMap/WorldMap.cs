using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WorldMap : MonoBehaviour {
    public static WorldMap Instance;

    public delegate void SelectedTileDelegate(WorldMapHexagonTile selectedTile);
    public event SelectedTileDelegate OnSelectedTileChanged;

    private WorldMapHexagonTile _selectedTile;
    public WorldMapHexagonTile SelectedTile
    {
        get { return _selectedTile; }
        set 
        {
            if (value != _selectedTile)
            {
                _selectedTile = value;
                if (OnSelectedTileChanged != null)
                {
                    OnSelectedTileChanged(_selectedTile);
                }
            }
        }
    }
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        WorldMapGen.Instance.GenerateMap();
        InitializeFogOfWar();
    }

    void InitializeFogOfWar()
    {
        foreach (WorldMapHexagonTile tile in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            tile.FogOfWar = true;
        }

        if (DataCarrier.PersistentData.WorldRepresentation.ContainsKey(new Vector2(0, 0)))
        {
            DataCarrier.PersistentData.WorldRepresentation[new Vector2(0, 0)].Visited = true;
        }
    }

    public void MapModeDefault()
    {
        Debug.Log("MapMode: Default");

        foreach (WorldMapHexagonTile tile in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            tile.Terrain = tile.Terrain;
            tile.Building = tile.Building;
        }
    }

    public void MapModeTileCoord()
    {
        Debug.Log("MapMode: Tile Coordinates");

        foreach (WorldMapHexagonTile tile in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            tile.Text.text = tile.TileCoordinate.ToString();
            tile.Sprite.color = tile.Terrain.Mask;
        }
    }

    public void MapModeVisisted()
    {
        Debug.Log("MapMode: Visited Tiles");

        foreach (WorldMapHexagonTile tile in DataCarrier.PersistentData.WorldRepresentation.Values)
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

    public void MapModeDifficulty()
    {
        Debug.Log("MapMode: Difficulty");
        
        float max = DataCarrier.PersistentData.WorldRepresentation.Values.ToList().Max(t => t.Difficulty);
        
        foreach (WorldMapHexagonTile tile in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            tile.Text.text = tile.Difficulty.ToString();
            tile.Sprite.color = new Color(
                1f,
                1f - tile.Difficulty / max,
                1f - tile.Difficulty / max);
        }
    }
}

