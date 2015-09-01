using UnityEngine;
using System.Collections;

public class Boulder : Obstacle {
    public float Damage = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Ship>() != null)
        {
            DataCarrier.SelectedShip.TakeDamage((int)(Damage));
        }
    }

    protected override void ResetAndRelease()
    {
        this.gameObject.SetActive(false);
        ObjectPool.Instance.Release<Boulder>(this);
    }
}
