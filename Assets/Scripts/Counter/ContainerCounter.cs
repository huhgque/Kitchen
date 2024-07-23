using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter 
{
    public static event EventHandler OnAnyContainerOpen;
    public event EventHandler OnContainerInteract;
    public override void Interact(Player player){
        if ( !player.HasKitchenObject() ){
            KitchenObject kitchenObject = Instantiate(kitchenSO.prefab.gameObject).GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(player);
            OnContainerInteract?.Invoke(this,EventArgs.Empty);
            OnAnyContainerOpen?.Invoke(this,EventArgs.Empty);
        }
    }
    public new void ClearKitchenObject(){
        return;
    }
}
