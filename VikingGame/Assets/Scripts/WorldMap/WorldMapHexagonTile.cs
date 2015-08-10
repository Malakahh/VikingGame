using UnityEngine;
using System.Collections;

public class WorldMapHexagonTile : MonoBehaviour
{
    public TextMesh Text;
    public SpriteRenderer Sprite;
    public SpriteRenderer Overlay;

    public Vector2 TileCoordinate;
    public WorldMapHexagonTile[] Neighbours = new WorldMapHexagonTile[6];
    
    public void SetTerrain(Sprite sprite)
    {
        SetTerrain(sprite, Color.white);
    }

    public void SetTerrain(Sprite sprite, Color mask)
    {
        this.Sprite.sprite = sprite;
        this.Sprite.color = mask;
    }
}
