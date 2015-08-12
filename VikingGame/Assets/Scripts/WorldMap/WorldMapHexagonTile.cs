using UnityEngine;
using System.Collections;

public class WorldMapHexagonTile : MonoBehaviour
{
    public TextMesh Text;
    public SpriteRenderer Sprite;
    public SpriteRenderer Overlay;

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
                if (_terrain.Sprite != null)
                {
                    this.Sprite.sprite = _terrain.Sprite;
                }

                if (_terrain.Mask != null)
                {
                    this.Sprite.color = _terrain.Mask;
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
                if (_building.Sprite != null)
                {
                    this.Sprite.sprite = _building.Sprite;
                }

                if (_building.Mask != null)
                {
                    this.Sprite.color = _building.Mask;
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
                foreach (WorldMapHexagonTile neighbour in this.Neighbours)
                {
                    neighbour.FogOfWar = false;
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
                this.Overlay.sprite = def.Sprite;
                this.Overlay.color = def.Mask;
            }
            else if (_fogOfWar)
            {
                this.Overlay.sprite = null;
                this.Overlay.color = new Color().RGB32(0xff, 0xff, 0xff);
            }
            _fogOfWar = value;
        }
    }
    
    private bool isHighlighted = false;

    void OnMouseEnter()
    {
        if (Overlay.sprite == null)
        {
            isHighlighted = true;
            OverlayDefinition def = WorldMap.Instance.Overlays[(int)OverlayDefinition.Type.Hover];
            this.Overlay.sprite = def.Sprite;
            this.Overlay.color = def.Mask;
        }
    }

    void OnMouseExit()
    {
        if (isHighlighted)
        {
            isHighlighted = false;
            this.Overlay.sprite = null;
        }
    }

    void OnMouseOver()
    {
        if (isHighlighted && Input.GetMouseButton(0))
        {
            if (!Visited)
            {
                Visited = true;
            }
        }
    }
}
