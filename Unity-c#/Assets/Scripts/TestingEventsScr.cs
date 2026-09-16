using UnityEngine;
using System;

public class TestingEventsScr : MonoBehaviour
{
    public event EventHandler OnSpacePressed;

    void Start()
    {
       // OnSpacePressed += Testing_OnSpacePressed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke(this, EventArgs.Empty);
        }
    }
    /*private void Testing_OnSpacePressed(object sender, EventArgs e)
    {
        Debug.Log("Space");
    }*/
        
}
