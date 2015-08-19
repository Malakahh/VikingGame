using UnityEngine;
using System.Collections.Generic;

public class RaidMapGen : MonoBehaviour {
    public static RaidMapGen Instance;

    public List<RaidMapTerrainDefinition> Terrain = new List<RaidMapTerrainDefinition>() {
        new RaidMapTerrainDefinition(TerrainDefinition.Type.Plains),
        new RaidMapTerrainDefinition(TerrainDefinition.Type.Forest),
        new RaidMapTerrainDefinition(TerrainDefinition.Type.Mountains)
    };
    
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

    int length;
    int terrainType;

    void Awake()
    {
        Instance = this;

        if (DataCarrier.SelectedTile == null)
        {
            CreateDummyData();
        }
    }

    void CreateDummyData()
    {
        DataCarrier.SelectedTile = ObjectPool.Acquire<WorldMapHexagonTile>();
        DataCarrier.SelectedTile.Terrain = new WorldMapTerrainDefinition(TerrainDefinition.Type.Forest);
        DataCarrier.SelectedTile.Difficulty = 1;
    }

    void Start()
    {
        SetupCamera();

        this.length = 100 + 5 * (int)(DataCarrier.SelectedTile.Difficulty + .5f);
        this.TotalWidth = (int)(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x + .5f) * 2;
        this.PlayableAreaWidth = TotalWidth - WallWidth * 2;
        this.terrainType = (int)DataCarrier.SelectedTile.Terrain.TerrainType;

        GenerateMap();
    }

    void SetupCamera()
    {
        //Camera.main.orthographicSize = 10;
    }

    void GenerateMap()
    {
        for (int y = 0; y < length; y++)
        {
            GenerateRow(new Vector3(0, y));
        }
    }

    void GenerateRow(Vector3 offset)
    {
        int halfWidth = TotalWidth / 2;

        for (int x = -halfWidth; x <= halfWidth; x++)
        {
            RaidMapTile t = ObjectPool.Acquire<RaidMapTile>();
            t.transform.position = new Vector3(x, offset.y, this.transform.position.z);

            if (x < -halfWidth + WallWidth || x > halfWidth - WallWidth) //Leftmost or rightmost tiles
            {
                t.Sprite.sprite = Terrain[terrainType].WallSprite;
                t.Sprite.color = Terrain[terrainType].WallMask;
            }
            else
            {
                t.Sprite.sprite = Terrain[terrainType].Sprite;
                t.Sprite.color = Terrain[terrainType].Mask;
            }
            
            t.gameObject.SetActive(true);
        }
    }
}

[System.Serializable]
public class RaidMapTerrainDefinition : TerrainDefinition
{
    public Sprite WallSprite;
    public Color WallMask = new Color().RGB32(0xFF, 0xFF, 0xFF);

    public RaidMapTerrainDefinition(Type TerrainType, Sprite Sprite, Sprite WallSprite) : base(TerrainType, Sprite)
    {
        this.WallSprite = WallSprite;
    }

    public RaidMapTerrainDefinition(Type TerrainType) : this(TerrainType, null, null)
    { }
}
