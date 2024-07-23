using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter,IProgressBarDisplay
{
    public static event EventHandler OnAnyStoveOn;
    public static event EventHandler OnAnyStoveOff;
    public event EventHandler<OnStoveStateChangeArgs> OnStoveStateChange;
    public class OnStoveStateChangeArgs:EventArgs{
        public CookingState cookingState;
    } 
    public enum CookingState{
        IDLE,
        COOKING,
        COOKED,
        BURNED
    }
    public event EventHandler<IProgressBarDisplay.ProgressBarArgs> OnProgressBarChange;
    [SerializeField] private RecipeSO[] recepiList;
    private CookingState cookingState;
    private float cookingProgress;
    private RecipeSO currentRecipe;
    public void Update(){
        switch (cookingState){
            case CookingState.IDLE:
                break;
            case CookingState.COOKING:
                if (!HasKitchenObject() || !currentRecipe) {
                    cookingState = CookingState.IDLE;
                }
                cookingProgress += Time.deltaTime;
                InvokeProgressBarEvent(cookingProgress / currentRecipe.GetProcessStep());
                if (cookingProgress > currentRecipe.GetProcessStep()){
                    cookingState = CookingState.COOKED;
                }
                break;
            case CookingState.COOKED:
                cookingProgress = 0;
                KitchenObject cookedObject = Instantiate(currentRecipe.GetOutput().prefab).GetComponent<KitchenObject>();
                kitchenObject.BecomeProcessed(cookedObject,this);
                currentRecipe = GetAvailableRecipe();
                if (currentRecipe){
                    cookingState = CookingState.COOKING;
                }else{
                    cookingState = CookingState.BURNED;
                }
                break;
            case CookingState.BURNED:
                OnStoveStateChange?.Invoke(this,new OnStoveStateChangeArgs{ cookingState = cookingState });
                OnAnyStoveOff?.Invoke(this,EventArgs.Empty);
                break;
        }
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() && !HasKitchenObject() ){
            // take object from player
            cookingProgress = 0;
            InvokeProgressBarEvent(0);
            player.GetKitchenObject().SetKitchenObjectParent(this);
            currentRecipe = GetAvailableRecipe();
        } else if (!player.HasKitchenObject() && HasKitchenObject()) {
            // give object to player
            cookingProgress = 0;
            cookingState = CookingState.IDLE;
            InvokeProgressBarEvent(0);
            kitchenObject.SetKitchenObjectParent(player);
            OnStoveStateChange?.Invoke(this,new OnStoveStateChangeArgs{ cookingState = cookingState });
            OnAnyStoveOff?.Invoke(this,EventArgs.Empty);
        }
    }
    public override void Use(Player player)
    {
        switch (cookingState) {
            case CookingState.IDLE :
                if(!HasKitchenObject()) return;
                cookingState = CookingState.COOKING;
                OnStoveStateChange?.Invoke(this,new OnStoveStateChangeArgs{ cookingState = cookingState });
                OnAnyStoveOn?.Invoke(this,EventArgs.Empty);
                break;
            case CookingState.BURNED:
            case CookingState.COOKING:
                cookingState = CookingState.IDLE;
                OnStoveStateChange?.Invoke(this,new OnStoveStateChangeArgs{ cookingState = cookingState });
                OnAnyStoveOff?.Invoke(this,EventArgs.Empty);
                break;
        }
    }
    private RecipeSO GetAvailableRecipe(){
        if (!HasKitchenObject()) return null;
        // find recipe
        foreach (RecipeSO recipe in recepiList ){
            // check recipe ingredient
            foreach(KitchenSO kitchenSO in recipe.GetInput()){
                if (kitchenSO == kitchenObject.GetKitchenSO()){
                    return recipe;
                }
            }
        }
        return null;
    }
    private void InvokeProgressBarEvent(float prog){
        OnProgressBarChange?.Invoke(this,new IProgressBarDisplay.ProgressBarArgs{progress = prog});
    }
}
