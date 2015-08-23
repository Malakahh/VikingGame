using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WorldMap : MonoBehaviour {
    public static WorldMap Instance;

    public delegate void SelectedTileDelegate(WorldMapHexagonTileData selectedTileData);
    public event SelectedTileDelegate OnSelectedTileChanged;

    private WorldMapHexagonTileData _selectedTileData;
    public WorldMapHexagonTileData SelectedTileData
    {
        get { return _selectedTileData; }
        set 
        {
            if (value != _selectedTileData)
            {
                _selectedTileData = value;
                if (OnSelectedTileChanged != null)
                {
                    OnSelectedTileChanged(_selectedTileData);
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
        RaidCompletionChecker.Instance.OnRaidSuccessful += Instance_OnRaidSuccessful;
    }

    void Instance_OnRaidSuccessful(WorldMapHexagonTileData tileData)
    {
        tileData.Visited = true;
    }

    public void MapModeDefault()
    {
        Debug.Log("MapMode: Default");

        foreach (WorldMapHexagonTileData tileData in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            tileData.Terrain = tileData.Terrain;
            tileData.Building = tileData.Building;
        }
    }

    public void MapModeTileCoord()
    {
        Debug.Log("MapMode: Tile Coordinates");

        foreach (WorldMapHexagonTileData tileData in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            tileData.Tile.Text.text = tileData.TileCoordinate.ToString();
            tileData.Tile.Sprite.color = tileData.Terrain.Mask;
        }
    }

    public void MapModeVisisted()
    {
        Debug.Log("MapMode: Visited Tiles");

        foreach (WorldMapHexagonTileData tileData in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            if (tileData.Visited)
            {
                tileData.Tile.Text.text = "Visited";
                tileData.Tile.Sprite.color = new Color().RGB32(0xAA, 0xAA, 0xAA);
            }
            else
            {
                tileData.Tile.Text.text = "Not\nVisisted";
                tileData.Tile.Sprite.color = new Color().RGB32(0x00, 0xFF, 0x00);
            }
        }
    }

    public void MapModeDifficulty()
    {
        Debug.Log("MapMode: Difficulty");

        float max = DataCarrier.PersistentData.WorldRepresentation.Values.ToList().Max(t => t.Difficulty);
        
        foreach (WorldMapHexagonTileData tileData in DataCarrier.PersistentData.WorldRepresentation.Values)
        {
            tileData.Tile.Text.text = tileData.Difficulty.ToString();
            tileData.Tile.Sprite.color = new Color(
                1f,
                1f - tileData.Difficulty / max,
                1f - tileData.Difficulty / max);
        }
    }

    void OnDestroy()
    {
        RaidCompletionChecker.Instance.OnRaidSuccessful -= Instance_OnRaidSuccessful;
    }
}