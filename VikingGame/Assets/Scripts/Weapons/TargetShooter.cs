using UnityEngine;
using System.Collections.Generic;

public class TargetShooter : Weapon {
    AmmunitionDamageType ammoDmgType;

    void Start()
    {
        this.ShootingFrequency = 2.5f;

        TargetShooterBullet bullet = ObjectPool.Instance.Acquire<TargetShooterBullet>();
        ammoDmgType = bullet.DamageType;
        ObjectPool.Instance.Release<TargetShooterBullet>(bullet);

    }

    protected override void Shoot(Obstacle target)
    {
        TargetShooterBullet bullet = ObjectPool.Instance.Acquire<TargetShooterBullet>();
        bullet.SetTrajectory(this, target.transform.position);
        bullet.gameObject.SetActive(true);
    }

    protected override Obstacle SelectBestTarget()
    {
        Obstacle target = null;
        float max = -1;
        foreach (Obstacle o in PossibleTargets)
        {
            DamageHelperTableEntry entry = DamageHelper.Instance.DamageTable.Find(x => x.ArmorType == o.ArmorType && x.DamageType == ammoDmgType);
            if (entry.MultiplicativeDamageModifier > max)
            {
                target = o;
                max = entry.MultiplicativeDamageModifier;
            }
        }

        return target;
    }
}
