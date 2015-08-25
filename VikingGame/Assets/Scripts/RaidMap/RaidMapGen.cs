using UnityEngine;
using System.Collections.Generic;

public class RaidMapGen : MonoBehaviour {
    public static RaidMapGen Instance;

    public List<RaidMapTerrainDefinition> Terrain = new List<RaidMapTerrainDefinition>() {
        new RaidMapTerrainDefinition(TerrainDefinition.Type.Plains),
        new RaidMapTerrainDefinition(TerrainDefinition.Type.Forest),
        new RaidMapTerrainDefinition(TerrainDefinition.Type.Mountains)
    };

    public List<RaidMapTile> Tiles = new List<RaidMapTile>();
    
    /// <summary>
    /// How many tiles on each edge are walls
    /// </summary>
    public int WallWidth = 2;

    /// <summary>
    /// Total width on map meassured in tiles
    /// </summary>
    public int TotalWidth { get; private set; }

    /// <summary>
    /// Play area width meassured in tiles (TotalWidth - WallWidth)
    /// </summary>
    public int PlayableAreaWidth { get; private set; }

    /// <summary>
    /// Gets the length of the map meassured in tiles
    /// </summary>
    public int Length { get; private set; }

    int terrainType;

    void Awake()
    {
        Instance = this;
    }

    void CreateDummyData()
    {
        Debug.LogWarning("RaidMapGen: Creating dummy data");
        WorldMapHexagonTile tile = ObjectPool.Instance.Acquire<WorldMapHexagonTile>();
        DataCarrier.SelectedTileData = tile.TileData;
        DataCarrier.SelectedTileData.Terrain = new WorldMapTerrainDefinition(TerrainDefinition.Type.Forest);
        DataCarrier.SelectedTileData.Difficulty = 1;
    }

    public void GenerateMap()
    {
        InitializeVariables();

        for (int y = 0; y < Length; y++)
        {
            GenerateRow(new Vector3(0, y));
        }
    }

    void InitializeVariables()
    {
        if (DataCarrier.SelectedTileData == null)
        {
            CreateDummyData();
        }

        SetupCamera();

        this.Length = 100 + 5 * (int)(DataCarrier.SelectedTileData.Difficulty + .5f);
        this.TotalWidth = (int)(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x + .5f) * 2;
        this.PlayableAreaWidth = TotalWidth - WallWidth * 2;
        this.terrainType = (int)DataCarrier.SelectedTileData.Terrain.TerrainType;
    }

    void SetupCamera()
    {
        //Camera.main.orthographicSize = 10;
        Camera.main.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height - Screen.height * 0.15f));
    }

    void GenerateRow(Vector3 offset)
    {
        int halfWidth = TotalWidth / 2;

        for (int x = -halfWidth; x <= halfWidth; x++)
        {
            RaidMapTile t = ObjectPool.Instance.Acquire<RaidMapTile>();
            t.transform.position = new Vector3(x, offset.y, this.transform.position.z);

            RaidMapTerrainDefinition def = Terrain[terrainType];

            if (x < -halfWidth + WallWidth || x > halfWidth - WallWidth) //Leftmost or rightmost tiles
            {
                t.Sprite.sprite = def.WallSprites[Random.Range(0, def.WallSprites.Count)];
                t.Sprite.color = def.WallMask;
            }
            else
            {
                t.Sprite.sprite = def.Sprites[Random.Range(0, def.Sprites.Count)];
                t.Sprite.color = def.Mask;
            }
            
            t.gameObject.SetActive(true);
            Tiles.Add(t);
        }
    }
}

[System.Serializable]
public class RaidMapTerrainDefinition : TerrainDefinition
{
    public List<Sprite> WallSprites;
    public Color WallMask = new Color().RGB32(0xFF, 0xFF, 0xFF);

    public RaidMapTerrainDefinition(Type TerrainType) : base(TerrainType)
    { }
}
