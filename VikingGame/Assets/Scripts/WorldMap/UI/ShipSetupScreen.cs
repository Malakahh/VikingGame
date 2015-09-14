using UnityEngine;
using System.Collections;

public class ShipSetupScreen : MonoBehaviour {

    void Awake()
    {
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }
}
