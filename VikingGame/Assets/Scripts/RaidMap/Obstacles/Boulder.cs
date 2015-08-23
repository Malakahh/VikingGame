using UnityEngine;
using System.Collections;

public class Boulder : Obstacle {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Ship>() != null)
        {
            DataCarrier.SelectedShip.TakeDamage(5);
        }
    }
}
