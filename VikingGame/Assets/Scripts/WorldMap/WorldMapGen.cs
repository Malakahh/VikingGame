using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMapGen : MonoBehaviour {
    float magicXOffset = 0.65f;
    float magixYOffset = 0.49f;
    int mapSizeX = 19;
    int mapSizeY = 19;

	void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        GenerateWorldModelRepresentation();
        MakeTerrain();
        MakeBuildings();
    }

    void GenerateWorldModelRepresentation()
    {
        GenerateHexagonTile(new Vector2(0, 0));
    }

    WorldMapHexagonTile GenerateHexagonTile(Vector2 TileCoord)
    {
        if (WorldMap.Instance.WorldRepresentation.ContainsKey(TileCoord))
        {
            return WorldMap.Instance.WorldRepresentation[TileCoord];
        }
        if (TileCoord.x < -(int)(mapSizeX / 2f - 0.5f) || TileCoord.x > mapSizeX / 2f)
        {
            return null;
        }
        if (TileCoord.y < -(int)(mapSizeY / 2f - 0.5f) || TileCoord.y > mapSizeY / 2f)
        {
            return null;
        }

        WorldMapHexagonTile tile = ObjectPool.Acquire<WorldMapHexagonTile>();
        tile.transform.parent = WorldMap.Instance.transform;
        tile.TileCoordinate = TileCoord;
        tile.transform.position = new Vector3(
            (TileCoord.y % 2 == 0) ? TileCoord.x * magicXOffset : TileCoord.x * magicXOffset + magicXOffset * 0.5f,
            TileCoord.y * magixYOffset,
            0);
        WorldMap.Instance.WorldRepresentation.Add(tile.TileCoordinate, tile);
        tile.gameObject.SetActive(true);

        for (int i = 0; i < tile.Neighbours.Length; i++)
        {
            Vector2 neighbourCoord = new Vector2();
            if (TileCoord.y % 2 == 0)
            {
                switch (i)
                {
                    case 0:
                        neighbourCoord = new Vector2(TileCoord.x, TileCoord.y + 1);
                        break;
                    case 1:
                        neighbourCoord = new Vector2(TileCoord.x + 1, TileCoord.y);
                        break;
                    case 2:
                        neighbourCoord = new Vector2(TileCoord.x, TileCoord.y - 1);
                        break;
                    case 3:
                        neighbourCoord = new Vector2(TileCoord.x - 1, TileCoord.y - 1);
                        break;
                    case 4:
                        neighbourCoord = new Vector2(TileCoord.x - 1, TileCoord.y);
                        break;
                    case 5:
                        neighbourCoord = new Vector2(TileCoord.x - 1, TileCoord.y + 1);
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        neighbourCoord = new Vector2(TileCoord.x + 1, TileCoord.y + 1);
                        break;
                    case 1:
                        neighbourCoord = new Vector2(TileCoord.x + 1, TileCoord.y);
                        break;
                    case 2:
                        neighbourCoord = new Vector2(TileCoord.x + 1, TileCoord.y - 1);
                        break;
                    case 3:
                        neighbourCoord = new Vector2(TileCoord.x, TileCoord.y - 1);
                        break;
                    case 4:
                        neighbourCoord = new Vector2(TileCoord.x - 1, TileCoord.y);
                        break;
                    case 5:
                        neighbourCoord = new Vector2(TileCoord.x, TileCoord.y + 1);
                        break;
                }
            }

            tile.Neighbours[i] = GenerateHexagonTile(neighbourCoord);
        }

        return tile;
    }

    void MakeTerrain()
    {
        List<int> list = new List<int>();
        WorldMap.Instance.Terrain.ForEach(x => list.Add(x.Weight));

        WeightedRandomizer randomizer = new WeightedRandomizer(list);
        foreach (WorldMapHexagonTile tile in WorldMap.Instance.WorldRepresentation.Values)
        {
            int ran = randomizer.GetRandomIndex();
            
            Color c = new Color();
            switch ((TerrainDefinition.Type)ran)
	        {
		        case TerrainDefinition.Type.Plains:
                    c = c.RGB32(0x00, 0xFF, 0x00);
                    break;
                case TerrainDefinition.Type.Forest:
                    c = c.RGB32(0x00, 0x66, 0x00);
                    break;
                case TerrainDefinition.Type.Mountains:
                    c = c.RGB32(0x99, 0x99, 0x99);
                    break;
	        }

            TerrainDefinition def = WorldMap.Instance.Terrain[ran];
            tile.SetTerrain(
                def.Sprite,
                c);
            tile.Text.text = def.Text;
        }
    }

    void MakeBuildings()
    {
        //Place tavern in center
        if (WorldMap.Instance.WorldRepresentation.ContainsKey(new Vector2(0,0)))
        {
            WorldMapHexagonTile tile = WorldMap.Instance.WorldRepresentation[new Vector2(0, 0)];
            tile.SetTerrain(
                WorldMap.Instance.Buildings[(int)BuildingDefinition.Type.Tavern].Sprite,
                new Color().RGB32(0x66, 0x33, 0x00));
            tile.Text.text = "Tavern";
        }
    }
}
