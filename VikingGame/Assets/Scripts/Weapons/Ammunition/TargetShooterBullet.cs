using UnityEngine;
using System.Collections;

public class TargetShooterBullet : MonoBehaviour {
    Vector3 trajectory;
    float travelSpeed = 25f;

	public void SetTrajectory(Vector3 Origin, Vector3 Target)
    {
        this.trajectory = Target - Origin;

        this.transform.position = Origin;

        if (this.trajectory != Vector3.zero)
        {
            float angle = Mathf.Atan2(this.trajectory.y, this.trajectory.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Update()
    {
        this.transform.position += trajectory.normalized * travelSpeed * TimeManager.GameplayTime.deltaTime;
    }

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
