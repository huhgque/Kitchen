using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverCounter : BaseCounter
{
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
            }else{
                Debug.Log("Gaf vl");
            }
        }
    }
}
