using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingOrderObject : MonoBehaviour
{
    public event EventHandler OnRecipeExpire;
    RecipeSO holdingRecipe;
    float waitTime = 0;
    float counter = 0;
    bool startCountDown = false;
    void Update() {
        if (!startCountDown) return;
        counter += Time.deltaTime;
        if (counter >= waitTime) {
            OnRecipeExpire?.Invoke(this,EventArgs.Empty);
        }
    }
    public void SetHoldingRecipe(RecipeSO recipe){
        holdingRecipe = recipe;
        waitTime = recipe.GetInput().Count * OrderManager.Instance.GetWaitTimePerIngredient();
    }
    public void SetStartCountDown(bool value){
        startCountDown = value;
    }
    public RecipeSO GetHoldingRecipe(){
        return holdingRecipe;
    }
}
