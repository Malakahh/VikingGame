﻿using UnityEngine;
using System.Collections;

public class RaidMap : MonoBehaviour {

    void Awake()
    {
        if (DataCarrier.SelectedShip == null)
        {
            CreateDummyData();
        }
    }
	
    void CreateDummyData()
    {
        Debug.LogWarning("RaidMap: Creating dummy data");
        DataCarrier.SelectedShip = ObjectPool.Instance.Acquire<Ship>();
    }

    void Start()
    {
        PlaceShip();
    }

    void PlaceShip()
    {
        DataCarrier.SelectedShip.transform.position = Vector3.zero;
        DataCarrier.SelectedShip.gameObject.SetActive(true);
    }

    void Update()
    {
        PlayerHasReachedGoal();
    }

    void PlayerHasReachedGoal()
    {
        if (DataCarrier.SelectedShip.transform.position.y >= RaidMapGen.Instance.Length)
        {
            DataCarrier.LoadWorldMapScene();
        }
    }
}