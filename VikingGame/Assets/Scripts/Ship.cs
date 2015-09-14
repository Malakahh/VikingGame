using UnityEngine;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
    public SpriteRenderer Sprite;
    public List<Vector3> WeaponPositionsRelative = new List<Vector3>();

    int _health = 100;
    public int Health
    {
        get { return _health; }
        private set { _health = value; }
    }

    public void TakeDamage(int dmg)
    {
        Health -= dmg;
        Debug.Log("Ship Damage taken: " + dmg + " Health: " + Health + "%");
    }
}
