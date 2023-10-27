using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public event EventHandler<OnOrderChangeArgs> OnFetchOrder;
    public event EventHandler<OnOrderChangeArgs> OnOrderComplete;
    public class OnOrderChangeArgs:EventArgs{
        public RecipeSO recipe;
    }
    public static OrderManager Instance {get; private set; }
    [SerializeField] private List<RecipeSO> recipelist;
    private List<RecipeSO> waitingOrder = new();

    private float fetchOrderCounter = 0f;
    private float fetchOrderColdown = 4f;
    private int maxOrder = 4;
    private void Awake() {
        Instance = this;
    }
    private void Update() {
        if (waitingOrder.Count >= maxOrder) return;
        fetchOrderCounter += Time.deltaTime;
        if (fetchOrderCounter > fetchOrderColdown){
            fetchOrderCounter = 0;
            RecipeSO recipe = recipelist[ UnityEngine.Random.Range(0,recipelist.Count) ];
            waitingOrder.Add(recipe);
            OnFetchOrder?.Invoke(this,new OnOrderChangeArgs{recipe = recipe});
        }
    }

    public bool CheckSubmitItem(List<KitchenSO> ingredient){
        foreach (RecipeSO recipe in recipelist){
            // if ingredient has the same amount of things as recipe
            if (recipe.GetInput().Count == ingredient.Count){
                // clone ingredient for easier validating
                // if ingredient has thing that recipe require, remove it from ingredient then continue
                // else break loop
                bool isCorrectIngredient = true;
                List<KitchenSO> checkingIngredient = new(ingredient);
                foreach (KitchenSO item in recipe.GetInput())
                {
                    if (checkingIngredient.Contains(item)){
                        checkingIngredient.Remove(item);
                    }else{
                        isCorrectIngredient = false;
                        break;
                    }
                }
                if (isCorrectIngredient){
                    OnOrderComplete?.Invoke(this,new OnOrderChangeArgs{recipe = recipe});
                    waitingOrder.Remove(recipe);
                    return true;
                }
            }else{
                continue;
            }
        }
        return false;
    }
}
