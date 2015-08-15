using UnityEngine;
using System.Collections.Generic;

public static class DataCarrier {
    public static class PersistentData
    {
        //Key: TileCoord (as opposed to world coord); value: Tile 
        public static Dictionary<Vector2, WorldMapHexagonTile> WorldRepresentation = new Dictionary<Vector2, WorldMapHexagonTile>();
    }

    public static WorldMapHexagonTile Tile;
    public static Ship Ship;

    public static void LoadGameplayScene(WorldMapHexagonTile tile, Ship ship)
    {
        Tile = tile;
        Ship = ship;

        //return tiles
        foreach (WorldMapHexagonTile t in PersistentData.WorldRepresentation.Values)
        {
            t.gameObject.SetActive(false);
            t.transform.parent = null;
            ObjectPool.Release<WorldMapHexagonTile>(t);
        }

        Application.LoadLevel("Gameplay");
    }

    public static void LoadWorldMapScene()
    {
        Application.LoadLevel("WorldMap");

        Dictionary<Vector2, WorldMapHexagonTile> temp = new Dictionary<Vector2,WorldMapHexagonTile>(PersistentData.WorldRepresentation);
        PersistentData.WorldRepresentation.Clear();

        foreach (Vector2 coord in temp.Keys)
        {
            WorldMapHexagonTile newTile = ObjectPool.Acquire<WorldMapHexagonTile>();
            newTile.CopyValues(temp[coord]);
            PersistentData.WorldRepresentation.Add(coord, newTile);
            newTile.gameObject.SetActive(true);
        }
    }
}
