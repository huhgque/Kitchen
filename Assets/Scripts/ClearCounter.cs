using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter 
{
    public override void Interact(Player player){
        if (player.HasKitchenObject() && !HasKitchenObject() ){
            player.GetKitchenObject().SetKitchenObjectParent(this);
        } else if (!player.HasKitchenObject() && HasKitchenObject()) {
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

}
