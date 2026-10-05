using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    [SerializeField]private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if(kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }
    public void Interact()
    {
        if (kitchenObject == null)
        {
            //Debug.Log("Interact");
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this); 

            //kitchenObjectTransform.localPosition = Vector3.zero;

            //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName); //

            //kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            //kitchenObject.SetClearCounter(this);
        } else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitcthenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject() 
    { 
        return kitchenObject != null; 
    }
}
