using UnityEngine;
using System.Collections;

public class WorldMapHexagonTile : MonoBehaviour
{
    public TextMesh Text;
    public SpriteRenderer Sprite;
    public SpriteRenderer FogOfWarRenderer;

    private WorldMapHexagonTileData _tileData = new WorldMapHexagonTileData();
    public WorldMapHexagonTileData TileData
    {
        get { return _tileData; }
        set 
        { 
            _tileData = value; 
            _tileData.Tile = this;
            _tileData.Terrain = _tileData.Terrain;
            _tileData.Building = _tileData.Building;
            _tileData.Visited = _tileData.Visited;
            _tileData.FogOfWar = _tileData.FogOfWar;
        }
    }

    public WorldMapHexagonTile[] Neighbours = new WorldMapHexagonTile[6];

    void Awake()
    {
        TileData.Tile = this;
    }
}

public class WorldMapHexagonTileData
{
    public WorldMapHexagonTile Tile;

    public Vector2 TileCoordinate;

    private WorldMapTerrainDefinition _terrain;
    public WorldMapTerrainDefinition Terrain
    {
        get { return _terrain; }
        set 
        { 
            _terrain = value;

            if (_terrain != null && Tile != null)
            {
                if (_terrain.Sprite != null && Tile.Sprite != null)
                {
                    Tile.Sprite.sprite = _terrain.Sprite;
                    Tile.Sprite.color = _terrain.Mask;
                }

                if (_terrain.Text != null && Tile.Text != null)
                {
                    Tile.Text.text = _terrain.Text;
                }
            }
        }
    }

    private BuildingDefinition _building;
    public BuildingDefinition Building
    {
        get { return _building; }
        set 
        { 
            _building = value;

            if (_building != null)
            {
                Tile.Sprite.color = _building.Mask;
                
                if (_building.Sprite != null)
                {
                    Tile.Sprite.sprite = _building.Sprite;
                }

                if (_building.Text != null)
                {
                    Tile.Text.text = _building.Text;
                }
            }
        }
    }

    private bool _visited;
    public bool Visited
    {
        get { return _visited; }
        set 
        { 
            _visited = value;
            if (_visited)
            {
                this.FogOfWar = false;

                for (int i = 0; i < Tile.Neighbours.Length; i++)
                {
                    if (Tile.Neighbours[i] != null)
                    {
                        Tile.Neighbours[i].TileData.FogOfWar = false;
                    }
                }
            }
        }
    }

    private bool _fogOfWar;
    public bool FogOfWar
    {
        get { return _fogOfWar; }
        set 
        { 
            if (Tile.FogOfWarRenderer != null)
            {
                if (value)
                {
                    OverlayDefinition def = WorldMapGen.Instance.Overlays[(int)OverlayDefinition.Type.FogOfWar];
                    Tile.FogOfWarRenderer.sprite = def.Sprite;
                    Tile.FogOfWarRenderer.color = def.Mask;
                }
                else if (_fogOfWar)
                {
                    Tile.FogOfWarRenderer.sprite = null;
                    Tile.FogOfWarRenderer.color = new Color().RGB32(0xff, 0xff, 0xff);
                }
            }
            _fogOfWar = value;
        }
    }

    private float _difficulty;
    public float Difficulty
    {
        get { return _difficulty; }
        set { _difficulty = value; }
    }
}