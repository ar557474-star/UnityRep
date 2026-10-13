using System;
using UnityEngine;
//using static CuttingCounter;

public class StoveCounter : BaseCounter,IHasProgress
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;

    public enum State
    {
        Idle,Frying,Fried,Burned
    }
    private State state;
    private float burningTimer;
    [SerializeField]private BurningRecipeSO[] burningRecipeSOArray;
    private BurningRecipeSO burningRecipeSO;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;



    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
            fryingTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimeMax
                });
                if (fryingTimer > fryingRecipeSO.fryingTimeMax)
            {
                    //fried
                    //Debug.Log("Fried");
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                    //Debug.Log("Fried! ");
                    state = State.Fried;
                    burningTimer = 0f;
                    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                }
                //Debug.Log(fryingTimer);

                break;
            case State.Fried:
                burningTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = burningTimer / burningRecipeSO.burningTimeMax
                });
                if (burningTimer > burningRecipeSO.burningTimeMax)
                {
                    //fried
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                    //Debug.Log("burned! ");
                    state = State.Burned;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0
                    });
                }
                break;
            case State.Burned:
                break;
        }//switch
        //Debug.Log(state);

    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())//no object here
        {
            if (player.HasKitchenObject())//player is carrying something
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))//if its from cutting recipe
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);//place it
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer/fryingRecipeSO.fryingTimeMax
                    });
                }
            }
            else
            {
                //player has nothing
            }
        }
        else //there is kitchen object here
        {
            if (player.HasKitchenObject())
            {//player is carrying 
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))//if its plate
                {
                    // PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else//nothing
            {
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }//interact

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
