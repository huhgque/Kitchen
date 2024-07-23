using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IProgressBarDisplay
{
    public static event EventHandler OnAnyCutting;
    public event EventHandler OnCutting;
    public event EventHandler<IProgressBarDisplay.ProgressBarArgs> OnProgressBarChange;

    [SerializeField] private RecipeSO[] recepiList;
    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() && !HasKitchenObject() ){
            // take object from player
            cuttingProgress = 0;
            InvokeProgressBarEvent(0);
            player.GetKitchenObject().SetKitchenObjectParent(this);
        } else if (!player.HasKitchenObject() && HasKitchenObject()) {
            // give object to player
            InvokeProgressBarEvent(0);
            kitchenObject.SetKitchenObjectParent(player);
        }
    }
    public override void Use(Player player)
    {
        RecipeSO recipe = GetAvailableRecipe();
        if (!recipe) return;
        cuttingProgress ++;
        OnCutting?.Invoke(this,EventArgs.Empty);
        InvokeProgressBarEvent((float) cuttingProgress / recipe.GetProcessStep());
        OnAnyCutting?.Invoke(this,EventArgs.Empty);
        if (cuttingProgress < recipe.GetProcessStep()) return;
        KitchenObject processedObject = Instantiate(recipe.GetOutput().prefab.gameObject).GetComponent<KitchenObject>();
        kitchenObject.BecomeProcessed(processedObject,this);
        InvokeProgressBarEvent(0);
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

    void InvokeProgressBarEvent(float prog)
    {
        OnProgressBarChange?.Invoke(this,new IProgressBarDisplay.ProgressBarArgs{progress = prog});
    }

}
