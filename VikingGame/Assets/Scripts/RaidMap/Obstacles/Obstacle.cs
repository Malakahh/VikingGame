﻿using UnityEngine;
using System.Collections;

public abstract class Obstacle : MonoBehaviour {
    private ObstacleArmorType _armorType = ObstacleArmorType.Unarmored;
    public ObstacleArmorType ArmorType
    {
        get { return _armorType; }
        set { _armorType = value; }
    }

    public const int MaxHealth = 100;

    private int _health = MaxHealth;
    public virtual int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public virtual void TakeDamage(int dmg)
    {
        Health -= dmg;

        if (Health <= 0)
        {
            Health = MaxHealth;
            ResetAndRelease();
        }

        Debug.Log("Obstacle Damage taken: " + dmg + " Health: " + Health + "%");
    }

    protected abstract void ResetAndRelease();
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