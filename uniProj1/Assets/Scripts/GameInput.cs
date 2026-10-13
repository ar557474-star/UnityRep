using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputAction plIpActions;
    private void Awake()
    {
        plIpActions = new PlayerInputAction();
        plIpActions.Player.Enable();

        plIpActions.Player.Interact.performed += InteractPerformed;
        plIpActions.Player.InteractAlternative.performed += InteractAlternative_performed;
    }//awake

    private void InteractAlternative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this , EventArgs.Empty);//this->sender is this script
    }


    public Vector2 GetMovementVector()
    {

        //Vector2 inputVector = new Vector2(0, 0);

        Vector2 inputVector = plIpActions.Player.Move.ReadValue<Vector2>();
/*
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("pressing");
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }
        */
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
