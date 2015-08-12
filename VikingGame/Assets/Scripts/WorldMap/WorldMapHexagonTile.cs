using UnityEngine;
using System.Collections;

public class WorldMapHexagonTile : MonoBehaviour
{
    public TextMesh Text;
    public SpriteRenderer Sprite;
    public SpriteRenderer FogOfWarRenderer;

    public Vector2 TileCoordinate;
    public WorldMapHexagonTile[] Neighbours = new WorldMapHexagonTile[6];

    private TerrainDefinition _terrain;
    public TerrainDefinition Terrain
    {
        get { return _terrain; }
        set 
        { 
            _terrain = value;

            if (_terrain != null)
            {
                this.Sprite.color = _terrain.Mask;

                if (_terrain.Sprite != null)
                {
                    this.Sprite.sprite = _terrain.Sprite;
                }

                if (_terrain.Text != null)
                {
                    this.Text.text = _terrain.Text;
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
                this.Sprite.color = _building.Mask;
                
                if (_building.Sprite != null)
                {
                    this.Sprite.sprite = _building.Sprite;
                }

                if (_building.Text != null)
                {
                    this.Text.text = _building.Text;
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

                for (int i = 0; i < this.Neighbours.Length; i++)
                {
                    if (this.Neighbours[i] != null)
                    {
                        this.Neighbours[i].FogOfWar = false;
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
            if (value)
            {
                OverlayDefinition def = WorldMap.Instance.Overlays[(int)OverlayDefinition.Type.FogOfWar];
                this.FogOfWarRenderer.sprite = def.Sprite;
                this.FogOfWarRenderer.color = def.Mask;
            }
            else if (_fogOfWar)
            {
                this.FogOfWarRenderer.sprite = null;
                this.FogOfWarRenderer.color = new Color().RGB32(0xff, 0xff, 0xff);
            }
            _fogOfWar = value;
        }
    }
}
