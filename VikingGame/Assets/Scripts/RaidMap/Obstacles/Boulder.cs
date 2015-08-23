using UnityEngine;
using System.Collections;

public class Boulder : Obstacle {

    void OnTriggerEnter2D(Collider2D other)
    {
        DataCarrier.SelectedShip.TakeDamage(5);
    }
}
