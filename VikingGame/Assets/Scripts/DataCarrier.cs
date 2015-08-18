using UnityEngine;
using System.Collections.Generic;

public static class DataCarrier {
    public static class PersistentData
    {
        //Key: TileCoord (as opposed to world coord); value: Tile 
        public static Dictionary<Vector2, WorldMapHexagonTile> WorldRepresentation = new Dictionary<Vector2, WorldMapHexagonTile>();
    }

    public static WorldMapHexagonTile SelectedTile;
    public static Ship SelectedShip;

    public static void LoadRaidMapScene(WorldMapHexagonTile tile, Ship ship)
    {
        SelectedTile = tile;
        SelectedShip = ship;

        //return tiles
        foreach (WorldMapHexagonTile t in PersistentData.WorldRepresentation.Values)
        {
            t.gameObject.SetActive(false);
            t.transform.parent = null;
            ObjectPool.Release<WorldMapHexagonTile>(t);
        }

        Application.LoadLevel("RaidMap");
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
