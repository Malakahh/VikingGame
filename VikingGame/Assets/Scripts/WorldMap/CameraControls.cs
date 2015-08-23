using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    int cameraBoundsExpansion = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
	}

    public void MoveUp()
    {
        if (Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y < WorldMapGen.Instance.MapHeight / 2 )
        {
            Camera.main.transform.position += Vector3.up;
        }
    }

    public void MoveDown()
    {
        if (Camera.main.ScreenToWorldPoint(Vector3.zero).y > -WorldMapGen.Instance.MapHeight / 2)
        {
            Camera.main.transform.position += Vector3.down;
        }
    }

    public void MoveLeft()
    {
        if (Camera.main.ScreenToWorldPoint(Vector3.zero).x > -WorldMapGen.Instance.MapWidth / 2 - cameraBoundsExpansion)
        {
            Camera.main.transform.position += Vector3.left;
        }
    }

    public void MoveRight()
    {
        if (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x < WorldMapGen.Instance.MapWidth / 2 + cameraBoundsExpansion)
        {
            Camera.main.transform.position += Vector3.right;
        }
    }
}
