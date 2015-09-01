using UnityEngine;
using System.Collections;

public class Target : Obstacle {
    protected override void ResetAndRelease()
    {
        this.gameObject.SetActive(false);
        ObjectPool.Instance.Release<Target>(this);
    }
}
