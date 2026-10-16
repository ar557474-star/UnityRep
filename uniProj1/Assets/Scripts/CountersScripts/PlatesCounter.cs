using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax=4f;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        //Debug.Log(spawnPlateTimer);
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            //KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
            spawnPlateTimer = 0f;
            if(plateSpawnAmount < plateSpawnAmountMax)
            {
                plateSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);

                //Debug.Log("spawnPlate " + plateSpawnAmount);
            }

        }
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(plateSpawnAmount> 0)
            {
                plateSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
