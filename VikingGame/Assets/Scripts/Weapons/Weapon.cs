using UnityEngine;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour {
    public enum WeaponAttachPoint { Left, Right };
    public BoxCollider2D WeaponCollider;

    public WeaponAttachPoint AttachPoint = WeaponAttachPoint.Left;

    public float ShootingFrequency = 1f;
    private float shootTime = 0;

    public List<Obstacle> PossibleTargets = new List<Obstacle>();
    Obstacle currentTarget;

	void Awake()
    {
        switch (AttachPoint)
        {
            case WeaponAttachPoint.Left:
                WeaponCollider.offset = new Vector2(-WeaponCollider.size.x * 0.5f, WeaponCollider.size.y * 0.5f);
                break;
            case WeaponAttachPoint.Right:
                WeaponCollider.offset = new Vector2(WeaponCollider.size.x * 0.5f, WeaponCollider.size.y * 0.5f);
                break;
        }
	}

    void Update()
    {
        shootTime += TimeManager.GameplayTime.deltaTime;
        if (shootTime >= 1/ShootingFrequency)
        {
            shootTime -= 1/ShootingFrequency;

            if (PossibleTargets.Count > 0)
            {
                if (!PossibleTargets.Contains(currentTarget))
                {
                    currentTarget = SelectBestTarget();
                }
                
                if (currentTarget != null)
                {
                    Shoot(currentTarget);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            PossibleTargets.Add(obstacle);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            PossibleTargets.Remove(obstacle);
        }
    }

    protected abstract Obstacle SelectBestTarget();    

    protected abstract void Shoot(Obstacle target);
}
