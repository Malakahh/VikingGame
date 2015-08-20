using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    public SpriteRenderer Sprite;

    int _hull = 100;
    public int Hull
    {
        get { return _hull; }
        private set { _hull = value; }
    }

    public void TakeDamage(int dmg)
    {
        Hull -= dmg;
        Debug.Log("Damage taken: " + dmg + " Hull: " + Hull + "%");
    }
}
