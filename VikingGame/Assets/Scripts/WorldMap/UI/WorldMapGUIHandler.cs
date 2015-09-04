using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldMapGUIHandler : MonoBehaviour {

    public Button StartRaidBtn;
    public Button EnterBuildingBtn;
    public GameObject TavernScreen;

	void Start()
    {
	    WorldMap.Instance.OnSelectedTileChanged += OnSelectedTileChanged;
	}
	
    void OnSelectedTileChanged(WorldMapHexagonTileData selectedTileData)
    {
        DisplayTileInformation(selectedTileData);
    }

    void DisplayTileInformation(WorldMapHexagonTileData selectedTileData)
    {
        Reset();

        if (!selectedTileData.Visited)
        {
            StartRaidBtn.gameObject.SetActive(true);
        }
        else if (selectedTileData.Visited && selectedTileData.Building != null)
        {
            EnterBuildingBtn.gameObject.SetActive(true);
        }
    }

    void Reset()
    {
        StartRaidBtn.gameObject.SetActive(false);
        EnterBuildingBtn.gameObject.SetActive(false);
    }

    public void BtnClickStartRaid()
    {
        DataCarrier.LoadRaidMapScene(WorldMap.Instance.SelectedTileData, null);
    }

    public void BtnClickEnterBuilding()
    {
        TavernScreen.SetActive(true);
    }
}
