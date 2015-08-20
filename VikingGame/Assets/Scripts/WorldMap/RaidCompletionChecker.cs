using UnityEngine;
using System.Collections;

public class RaidCompletionChecker : MonoBehaviour {
    public static RaidCompletionChecker Instance;

    public delegate void RaidCompletionDelegate(WorldMapHexagonTileData tileData);
    public event RaidCompletionDelegate OnRaidSuccessful;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(PerformChecks());
    }

    IEnumerator PerformChecks()
    {
        yield return 0;

        if (DataCarrier.SelectedTileData != null)
        {
            if (DataCarrier.RaidSuccessful)
            {
                DataCarrier.RaidSuccessful = false;

                if (OnRaidSuccessful != null)
                {
                    OnRaidSuccessful(DataCarrier.SelectedTileData);
                }
            }

            DataCarrier.SelectedTileData = null;
        }
    }
}
