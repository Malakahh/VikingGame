using UnityEngine;
using System.Collections;

public abstract class Obstacle : MonoBehaviour {
    private ObstacleArmorType _damageType = ObstacleArmorType.Unarmored;
    public ObstacleArmorType DamageType
    {
        get { return _damageType; }
        set { _damageType = value; }
    }

    private int _health = 100;
    public virtual int Health
    {
        get { return _health; }
        set { _health = value; }
    }
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