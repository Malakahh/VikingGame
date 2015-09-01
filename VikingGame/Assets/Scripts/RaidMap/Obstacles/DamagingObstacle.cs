using UnityEngine;
using System.Collections;

public abstract class DamagingObstacle : Obstacle {
    public float Damage = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        Ship ship = other.GetComponent<Ship>();
        if (ship != null)
        {
            ship.TakeDamage((int)(Damage));
        }
    }
	
}
