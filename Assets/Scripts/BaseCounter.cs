using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour , IKitchenObjectParent
{
    [SerializeField] protected KitchenSO kitchenSO;
    [SerializeField] protected GameObject tomatoSpawn;

    protected KitchenObject kitchenObject;
    public abstract void Interact(Player player);
    public virtual void Use(Player player){
        return;
    }
    public KitchenSO GetKitchenSO(){
        return kitchenSO;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }
    

    public GameObject GetObjectDisplayPosition()
    {
        return tomatoSpawn;
    }
}
