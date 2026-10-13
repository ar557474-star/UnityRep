using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [Serializable]public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSoGameObjectList;
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIndredientAdded;
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSoGameObjectList)
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIndredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSoGameObjectList)
        {
            if(kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
