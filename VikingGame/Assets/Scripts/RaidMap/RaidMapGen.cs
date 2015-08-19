using UnityEngine;
using System.Collections.Generic;

public class RaidMapGen : MonoBehaviour {
    public List<RaidMapTerrainDefinition> Terrain = new List<RaidMapTerrainDefinition>() {
        new RaidMapTerrainDefinition(TerrainDefinition.Type.Plains),
        new RaidMapTerrainDefinition(TerrainDefinition.Type.Forest),
        new RaidMapTerrainDefinition(TerrainDefinition.Type.Mountains)
    };
    
    int length;
    float width;
    int terrainType;

    void Awake()
    {
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

        this.length = 50 + 1 * (int)(DataCarrier.SelectedTile.Difficulty + .5f);
        this.width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x;
        this.terrainType = (int)DataCarrier.SelectedTile.Terrain.TerrainType;

        GenerateMap();
    }

    void SetupCamera()
    {

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
        for (int x = -(int)(width + .5f); x < width + 0.5f; x++)
        {
            RaidMapTile t = ObjectPool.Acquire<RaidMapTile>();
            t.transform.position = new Vector3(x, offset.y, this.transform.position.z);

            if (x < -width || x > width) //Leftmost or rightmost tile
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
