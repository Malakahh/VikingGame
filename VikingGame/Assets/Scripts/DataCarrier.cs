using UnityEngine;
using System.Collections.Generic;

public static class DataCarrier {
    public static class PersistentData
    {
        //Key: TileCoord (as opposed to world coord); value: Tile 
        public static Dictionary<Vector2, WorldMapHexagonTileData> WorldRepresentation = new Dictionary<Vector2, WorldMapHexagonTileData>();
    }

    public static WorldMapHexagonTileData SelectedTileData;
    public static Ship SelectedShip;
    public static bool RaidSuccessful = false;

    public static void LoadRaidMapScene(WorldMapHexagonTileData tileData, Ship ship)
    {
        SelectedTileData = tileData;
        SelectedShip = ship;

        Application.LoadLevel("RaidMap");
    }

    public static void LoadWorldMapScene()
    {
        Application.LoadLevel("WorldMap");

        RaidSuccessful = true;
    }
}
