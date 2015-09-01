using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    public SpriteRenderer Sprite;

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
