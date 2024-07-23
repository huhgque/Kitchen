using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public static event EventHandler OnAnyItemPlace;
    
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject() && HasKitchenObject()){
            if (TryAddPlayerPlate(player)){}
            else if (TryAddSelfPlate(player)){
                OnAnyItemPlace?.Invoke(this,EventArgs.Empty);
            }
        } else {
            if (TryGiveToPlayer(player)){}
            else if (TryTakeFromPlayer(player)){
                OnAnyItemPlace?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    private bool TryAddPlayerPlate(Player player){  
        // if player is holding plate and self does not hold plate
        if(kitchenObject is PlateKitchenObject) return false;
        if(player.GetKitchenObject() is not PlateKitchenObject) return false;
        PlateKitchenObject plate = player.GetKitchenObject() as PlateKitchenObject;
        plate.TryAddIngredient(kitchenObject);
        Destroy(kitchenObject.gameObject);
        return true;
    }
    private bool TryAddSelfPlate(Player player){
        if(kitchenObject is not PlateKitchenObject) return false;
        if(player.GetKitchenObject() is PlateKitchenObject) return false;
        PlateKitchenObject plate = kitchenObject as PlateKitchenObject;
        plate.TryAddIngredient(player.GetKitchenObject());
        Destroy(player.GetKitchenObject().gameObject);
        return true;
    }
    private bool TryTakeFromPlayer(Player player){
        if(!player.HasKitchenObject()) return false;
        if(HasKitchenObject()) return false;
        player.GetKitchenObject().SetKitchenObjectParent(this);
        return true;
    }
    private bool TryGiveToPlayer(Player player){
        if(player.HasKitchenObject()) return false;
        if(!HasKitchenObject()) return false;
        kitchenObject.SetKitchenObjectParent(player);
        return true;
    }
}
