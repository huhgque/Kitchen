using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedArgs> OnIngredientAdded;
    public class OnIngredientAddedArgs:EventArgs{
        public KitchenSO ingredient;
    }
    [SerializeField] private List<KitchenObject> validIngredientList = new();
    private List<KitchenSO> ingredientList = new();

    public bool TryAddIngredient(KitchenObject ingredient){
        if (validIngredientList.Find( i => i.GetKitchenSO() == ingredient.GetKitchenSO() )){
            ingredientList.Add(ingredient.GetKitchenSO());
            OnIngredientAdded?.Invoke(this,new OnIngredientAddedArgs{ingredient = ingredient.GetKitchenSO()});
            return true;
        }
        return false;
    }
    public List<KitchenSO> GetIngredientList(){
        return ingredientList;
    }
}
