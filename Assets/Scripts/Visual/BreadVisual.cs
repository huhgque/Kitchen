using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenSo_BreadVisual{
        public GameObject ingredientVisual;
        public KitchenSO ingredient;
    }
    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private List<KitchenSo_BreadVisual> kitchenSo_BreadVisuals;
    private void Start() {
        plate.OnIngredientAdded += HandleIngredientAdded;
    }

    private void HandleIngredientAdded(object sender,PlateKitchenObject.OnIngredientAddedArgs args){
        KitchenSo_BreadVisual bread = kitchenSo_BreadVisuals.Find( b => b.ingredient == args.ingredient);
        if (!bread.Equals(default(KitchenSo_BreadVisual))){
            bread.ingredientVisual.SetActive(true);
        }
    }

}
