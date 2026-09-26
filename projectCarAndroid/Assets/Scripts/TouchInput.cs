using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public bool accelerate;
    public bool brake;
    public bool moveLeft;
    public bool moveRight;

    public void AccelerateButtonDown()
    {
        accelerate = true;
    }
    public void AccelerateButtonUp()
    {
        accelerate = false;
    }
    public void BrakeButtonDown()
    {
        brake = true;
    }
    public void BrakeButtonUp()
    {
        brake = false;
    }
    public void MoveLeftButtonDown()
    {
        moveLeft = true;
    }
    public void MoveLeftButtonUp()
    {
        moveLeft = false;
    }
    public void MoveRightButtonDown()
    {
        moveRight = true;
    }
    public void MoveRightButtonUp()
    {
        moveRight = false;
    }
}
