using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {
    public enum WeaponAttachPoint { Left, Right };
    public BoxCollider2D collider;

    public WeaponAttachPoint AttachPoint = WeaponAttachPoint.Left;

	// Use this for initialization
	void Start () {
        switch (AttachPoint)
        {
            case WeaponAttachPoint.Left:
                collider.offset = new Vector2(-collider.size.x * 0.5f, collider.size.y * 0.5f);
                break;
            case WeaponAttachPoint.Right:
                collider.offset = new Vector2(collider.size.x * 0.5f, collider.size.y * 0.5f);
                break;
        }
	}
}
