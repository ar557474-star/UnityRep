using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputAction plIpActions;
    public event EventHandler OnPauseAction;

    public static GameInput Instance {  get; private set; }

    public enum Binding
    {
        MoveUp,MoveDown,MoveLeft,MoveRight,Interact,InteractAlternate,Pause,GamepadInteract, GamepadInteractAlternate,GamepadPause
    }
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    private void Awake()
    {
        Instance = this;
        plIpActions = new PlayerInputAction();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            plIpActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
        plIpActions.Player.Enable();

        plIpActions.Player.Interact.performed += InteractPerformed;
        plIpActions.Player.InteractAlternative.performed += InteractAlternative_performed;
        plIpActions.Player.Pause.performed += Pause_performed;

        /* Debug.Log(GetBindingText(Binding.MoveUp));
         Debug.Log(GetBindingText(Binding.MoveDown));
         Debug.Log(GetBindingText(Binding.MoveLeft));
         Debug.Log(GetBindingText(Binding.MoveRight));*/

    }//awake
    private void Start()
    {
        //Instance = this;
    }
    private void OnDestroy()
    {
        plIpActions.Player.Interact.performed -= InteractPerformed;
        plIpActions.Player.InteractAlternative.performed -= InteractAlternative_performed;
        plIpActions.Player.Pause.performed -= Pause_performed;

        plIpActions.Dispose();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //Debug.Log("esc");
        OnPauseAction?.Invoke(this, EventArgs.Empty);
        //Debug.Log("invoke");
    }

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
    public string GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default:
            case Binding.MoveUp:
                return plIpActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return plIpActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return plIpActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return plIpActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return plIpActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return plIpActions.Player.InteractAlternative.bindings[0].ToDisplayString();
            case Binding.Pause:
                return plIpActions.Player.Pause.bindings[0].ToDisplayString();
            case Binding.GamepadInteract:
                return plIpActions.Player.Interact.bindings[1].ToDisplayString();
            case Binding.GamepadInteractAlternate:
                return plIpActions.Player.InteractAlternative.bindings[1].ToDisplayString();
            case Binding.GamepadPause:
                return plIpActions.Player.Pause.bindings[1].ToDisplayString();
        }
    }
    public void RebindBinding(Binding binding,Action onActionRebound)
    {
        plIpActions.Player.Disable();
        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                inputAction = plIpActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = plIpActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = plIpActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = plIpActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = plIpActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = plIpActions.Player.InteractAlternative;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = plIpActions.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.GamepadInteract:
                inputAction = plIpActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.GamepadInteractAlternate:
                inputAction = plIpActions.Player.InteractAlternative;
                bindingIndex = 1;
                break;
            case Binding.GamepadPause:
                inputAction = plIpActions.Player.Pause;
                bindingIndex = 1;
                break;

        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            //Debug.Log(callback.action.bindings[1].path);
            //Debug.Log(callback.action.bindings[1].overridePath);
            callback.Dispose();
            plIpActions.Player.Enable();
            onActionRebound();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, plIpActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }
}
