using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventsArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventsArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }


    [SerializeField] private float moveSpeed = 3f;
    private bool isWalking;

    [SerializeField] private GameInput gameInput;
    private Vector3 lastInteractDir;
    [SerializeField] private LayerMask countersLayerMask;

    private ClearCounter selectedCounter;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("more than one player");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInputOnInteractAction;
    }

    private void GameInputOnInteractAction(object sender, EventArgs e)
    {

        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();


    }//update

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDir!= Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance =2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit,interactDistance, countersLayerMask))
        {
            //Debug.Log(hit.transform);//
            if(hit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //selectedCounter = clearCounter;
                if(clearCounter != selectedCounter)
                {
                 //   selectedCounter = clearCounter;
                    SetSelectedCounter(clearCounter);

                }
            } else
            {
                // selectedCounter=null;
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
       // Debug.Log("selected counter "+selectedCounter);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // bool canMove = !Physics.Raycast(transform.position, moveDir, 0.7f);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .6f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * 10);//rotate in move dir
        //transform.forward = moveDir;
    }
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventsArgs
        {
            selectedCounter = selectedCounter
        });
    }

}
