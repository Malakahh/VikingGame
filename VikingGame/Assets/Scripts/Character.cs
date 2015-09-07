using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    private bool _owned;
    public bool Owned
    {
        get { return _owned; }
        set { _owned = value; }
    }

    public Sprite Model;
    public Sprite Icon;
    public Weapon Weapon;
}
