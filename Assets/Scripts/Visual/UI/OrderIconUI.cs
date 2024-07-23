using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderIconUI : MonoBehaviour
{
    public WaitingOrderObject refObject {get;set;}
    public RecipeSO recipe {get;set;}
    [SerializeField] private IconUI iconTemplate;
    List<IconUI> iconUIs= new List<IconUI>();
    bool isHide = false;
    public void AddRecipe(RecipeSO recipeSO){
        recipe = recipeSO;
        bool isFirstIcon = true;
        foreach(KitchenSO kitchenSO in recipe.GetInput()){
            IconUI icon = Instantiate(iconTemplate,transform);
            icon.TryAddIcon(kitchenSO);
            icon.gameObject.SetActive(true);
            iconUIs.Add(icon);
            if (isFirstIcon){
                isFirstIcon = false;
                icon.SetWaitTime(recipe.GetInput().Count * OrderManager.Instance.GetWaitTimePerIngredient());
                icon.StartCountDown();
            }
        }
    }
    public void HideIcon(){
        if (isHide){
            isHide = false;
            foreach (var item in iconUIs)
            {
                item.gameObject.SetActive(true);
                
            }
        } else {
            isHide = true;
            int count = 0;
            foreach (var item in iconUIs)
            {
                count++;
                if (count == 1) continue;
                else {
                    item.gameObject.SetActive(false);
                }
            }
        }
    }
}
