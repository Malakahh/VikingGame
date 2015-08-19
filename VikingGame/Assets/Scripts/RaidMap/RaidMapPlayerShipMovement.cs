using UnityEngine;
using System.Collections;

public class RaidMapPlayerShipMovement : MonoBehaviour {
    public int ForwardSpeed = 1;
    public int SteerSpeed = 1;

    void Update()
    {
        ForwardMovement();
        Steer();
    }

    void Steer()
    {
        Vector3 increment = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            increment += Vector3.left * SteerSpeed * TimeManager.GameplayTime.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            increment += Vector3.right * SteerSpeed * TimeManager.GameplayTime.deltaTime;
        }

        //Todo: Make checks to constrain ship to map

        this.transform.position += increment;
    }

    void ForwardMovement()
    {
        Vector3 xIncrement = Vector3.up * ForwardSpeed * TimeManager.GameplayTime.deltaTime;
        transform.position += xIncrement;
        Camera.main.transform.position += xIncrement;
    }
}
