using UnityEngine;
using System.Collections;

public class Boulder : DamagingObstacle {
    void Start()
    {
        this.Damage = 5f;
        this.ArmorType = ObstacleArmorType.NaturalEnvironment;
    }

    protected override void ResetAndRelease()
    {
        this.gameObject.SetActive(false);
        ObjectPool.Instance.Release<Boulder>(this);
    }
}
