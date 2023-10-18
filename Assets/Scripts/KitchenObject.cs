using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenSO kitchenSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenSO GetKitchenSO(){
        return kitchenSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent parent){
        if (HasParent()){
            kitchenObjectParent.ClearKitchenObject();
        }
        kitchenObjectParent = parent;
        parent.SetKitchenObject(this);
        transform.parent = parent.GetObjectDisplayPosition().transform;
        transform.localPosition = Vector3.zero;
    }
    public void BecomeProcessed(KitchenObject processed,IKitchenObjectParent parent){
        processed.SetKitchenObjectParent(parent);
        Destroy(gameObject);
    }
    public bool HasParent(){
        return kitchenObjectParent != null;
    }
}
