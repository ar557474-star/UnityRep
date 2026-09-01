using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private bool isWalking;

    [SerializeField] private GameInput gameInput;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;
    private void Awake()
    {

    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInputOnInteractAction;
    }

    private void GameInputOnInteractAction(object sender, EventArgs e)
    {
       if(selectedCounter != null)
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

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, 2f))
        {
            //Debug.Log(hit.transform);
            if(hit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                    selectedCounter = clearCounter;
                } else
                {
                    selectedCounter = null;
                }

            } else
            {
                selectedCounter = null;
            }

            
        } 
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

}
