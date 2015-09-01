using UnityEngine;
using System.Collections;

public abstract class Ammunition : MonoBehaviour {
    private AmmunitionDamageType _damageType = AmmunitionDamageType.Piercing;
    public virtual AmmunitionDamageType DamageType
    {
        get { return _damageType; }
        set { _damageType = value; }
    }

    private float _travelSpeed = 25f;
    public virtual float TravelSpeed
    {
        get { return _travelSpeed; }
        set { _travelSpeed = value; }
    }

    private float _damage = 1f;
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    protected Weapon weaponSource;
    Vector3 trajectory;

    public virtual void SetTrajectory(Weapon Source, Vector3 Target)
    {
        this.weaponSource = Source;
        this.trajectory = Target - this.weaponSource.transform.position;
        this.transform.position = this.weaponSource.transform.position;

        if (this.trajectory != Vector3.zero)
        {
            float angle = Mathf.Atan2(this.trajectory.y, this.trajectory.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Update()
    {
        this.transform.position += trajectory.normalized * TravelSpeed * TimeManager.GameplayTime.deltaTime;
    }
}
