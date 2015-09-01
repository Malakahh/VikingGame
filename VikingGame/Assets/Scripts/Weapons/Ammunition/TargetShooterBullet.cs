using UnityEngine;
using System.Collections;

public class TargetShooterBullet : Ammunition {
    void Start()
    {
        this.Damage = 50;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Obstacle target = other.GetComponent<Obstacle>();
        if (target != null)
        {
            DamageHelperTableEntry entry = DamageHelper.Instance.DamageTable.Find(x => x.ArmorType == target.ArmorType && x.DamageType == this.DamageType);
            target.TakeDamage((int)(this.Damage * entry.MultiplicativeDamageModifier), weaponSource);
            this.gameObject.SetActive(false);
            ObjectPool.Instance.Release<TargetShooterBullet>(this);
        }
    }
}
