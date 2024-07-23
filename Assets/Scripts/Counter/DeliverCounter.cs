using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverCounter : BaseCounter
{
    public static event EventHandler OnAnyDeliverySuccess;
    public static event EventHandler OnAnyDeliveryFail;
    private OrderManager orderManager;
    private void Start() {
        orderManager = OrderManager.Instance;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() && player.GetKitchenObject() is PlateKitchenObject){
            PlateKitchenObject plate = player.GetKitchenObject() as PlateKitchenObject;
            bool isCorrectRecipe = orderManager.CheckSubmitItem(plate.GetIngredientList());
            if(isCorrectRecipe){
                Debug.Log("Correct");
                Destroy(player.GetKitchenObject().gameObject);
                OnAnyDeliverySuccess?.Invoke(this,EventArgs.Empty);
            }else{
                Debug.Log("Gaf vl");
                OnAnyDeliveryFail?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}
