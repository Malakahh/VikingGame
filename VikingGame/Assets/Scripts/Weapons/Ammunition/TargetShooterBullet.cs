using UnityEngine;
using System.Collections;

public class TargetShooterBullet : Ammunition {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Target>() != null)
        {
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            ObjectPool.Instance.Release<TargetShooterBullet>(this);
        }
    }
}
