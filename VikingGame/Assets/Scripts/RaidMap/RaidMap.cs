using UnityEngine;
using System.Collections;

public class RaidMap : MonoBehaviour {

    void CreateDummyData()
    {
        Debug.LogWarning("RaidMap: Creating dummy data");
        DataCarrier.SelectedShip = ObjectPool.Instance.Acquire<Ship>();
        TargetShooter shooter1 = ObjectPool.Instance.Acquire<TargetShooter>();
        //TargetShooter shooter2 = ObjectPool.Instance.Acquire<TargetShooter>();

        shooter1.AttachPoint = Weapon.WeaponAttachPoint.Left;
        //shooter2.AttachPoint = Weapon.WeaponAttachPoint.Right;

        shooter1.transform.parent = DataCarrier.SelectedShip.transform;
        //shooter2.transform.parent = DataCarrier.SelectedShip.transform;

        shooter1.gameObject.SetActive(true);
        //shooter2.gameObject.SetActive(true);
    }

    void Start()
    {
        if (DataCarrier.SelectedShip == null)
        {
            CreateDummyData();
        }

        RaidMapGen.Instance.GenerateMap();
        RaidMapObstacleGenerator.Instance.SpawnObstacles();
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
