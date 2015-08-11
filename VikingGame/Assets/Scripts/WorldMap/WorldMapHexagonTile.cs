using UnityEngine;
using System.Collections;

public class WorldMapHexagonTile : MonoBehaviour
{
    public TextMesh Text;
    public SpriteRenderer Sprite;
    public SpriteRenderer Overlay;

    public Vector2 TileCoordinate;
    public WorldMapHexagonTile[] Neighbours = new WorldMapHexagonTile[6];

    public bool Visited { get; set; }

    private bool isHighlighted = false;

    public void SetTerrain(Sprite sprite)
    {
        SetTerrain(sprite, Color.white);
    }

    public void SetTerrain(Sprite sprite, Color mask)
    {
        this.Sprite.sprite = sprite;
        this.Sprite.color = mask;
    }

    void OnMouseEnter()
    {
        if (Overlay.sprite == null)
        {
            isHighlighted = true;
            OverlayDefinition def = WorldMap.Instance.Overlays[(int)OverlayDefinition.Type.Hover];
            this.Overlay.sprite = def.Sprite;
            this.Overlay.color = def.Color;
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
                WorldMap.Instance.UpdateFogOfWar();
            }
        }
    }
}
