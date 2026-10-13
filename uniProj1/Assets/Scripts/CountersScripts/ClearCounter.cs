using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;

    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())//no object here
        {
            if (player.HasKitchenObject())//player is carrying something
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else
            {
                //player has nothing
            }


        }
        else //there is kitchen object here
        {
            if (player.HasKitchenObject())
            {//player is carrying 
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))//if its plate
                {
                   // PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }
                } else //player is holding something except plate
                {
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))//counter is holding a plate
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }

            } else//nothing
            {
                GetKitchenObject().SetKitchenObjectParent(player); 
            }
        }
    }

    

}
