using UnityEngine;
using System.Collections;

public class TargetShooter : Weapon {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Target>() != null)
        {
            
        }
    }
}
