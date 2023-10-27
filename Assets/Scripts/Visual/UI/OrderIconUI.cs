using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class OrderIconUI : MonoBehaviour
{
    public RecipeSO recipe {get;set;}
    [SerializeField] private IconUI iconTemplate;
    public void AddRecipe(RecipeSO recipeSO){
        recipe = recipeSO;
        foreach(KitchenSO kitchenSO in recipe.GetInput()){
            IconUI icon = Instantiate(iconTemplate,transform);
            icon.TryAddIcon(kitchenSO);
            icon.gameObject.SetActive(true);
        }
    }
}
