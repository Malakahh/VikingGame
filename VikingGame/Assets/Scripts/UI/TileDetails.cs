using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileDetails : MonoBehaviour {

    public Button StartRaidBtn;

	void Start()
    {
	    WorldMap.Instance.OnSelectedTileChanged += OnSelectedTileChanged;
	}
	
    void OnSelectedTileChanged(WorldMapHexagonTile selectedTile)
    {
        StartRaidBtn.gameObject.SetActive(true);
    }

    public void StartRaidBtnClick()
    {
        DataCarrier.LoadRaidMapScene(WorldMap.Instance.SelectedTile, null);
    }
}
