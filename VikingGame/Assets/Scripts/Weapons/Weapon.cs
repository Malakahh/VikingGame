using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {
    public enum WeaponAttachPoint { Left, Right };
    public BoxCollider2D WeaponCollider;

    public WeaponAttachPoint AttachPoint = WeaponAttachPoint.Left;

	// Use this for initialization
	void Start () {
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
}
