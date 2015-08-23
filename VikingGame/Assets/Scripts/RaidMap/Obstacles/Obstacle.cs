using UnityEngine;
using System.Collections;

public abstract class Obstacle : MonoBehaviour {
    
}

public class ObstacleMetaData
{
    public delegate Obstacle ObstacleDelegate();

    public ObstacleDelegate SpawnObstacle;
    public float Cost;
    public int Weight;

    public ObstacleMetaData(float Cost, int Weight, ObstacleDelegate SpawnObstacle)
    {
        this.Cost = Cost;
        this.Weight = Weight;
        this.SpawnObstacle = SpawnObstacle;
    }
}