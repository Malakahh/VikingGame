using UnityEngine;
using System.Collections;

public class TargetShooter : Weapon {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Target>() != null)
        {
            TargetShooterBullet bullet = ObjectPool.Instance.Acquire<TargetShooterBullet>();
            bullet.SetTrajectory(this.transform.position, other.transform.position);
            bullet.gameObject.SetActive(true);
        }
    }
}
