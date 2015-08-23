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

        if (this.transform.position.x + increment.x < -RaidMapGen.Instance.PlayableAreaWidth / 2 ||
            this.transform.position.x + increment.x > RaidMapGen.Instance.PlayableAreaWidth / 2)
        {
            increment.x = 0;
        }

        this.transform.position += increment;
    }

    void ForwardMovement()
    {
        Vector3 yIncrement = Vector3.up * ForwardSpeed * TimeManager.GameplayTime.deltaTime;
        transform.position += yIncrement;
        Camera.main.transform.position += yIncrement;
    }
}
