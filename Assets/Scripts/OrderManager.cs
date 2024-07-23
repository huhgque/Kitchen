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
        public WaitingOrderObject order;
    }
    public static OrderManager Instance {get; private set; }
    [SerializeField] private List<RecipeSO> recipelist;
    [SerializeField] float waitTimePerIngredient = 5f;
    [SerializeField] WaitingOrderObject waitingOrderObjectTemplate;
    List<WaitingOrderObject> waitingOrderObjects= new();
    public float GetWaitTimePerIngredient() {return waitTimePerIngredient;}
    private float fetchOrderCounter = 0f;
    private float fetchOrderColdown = 4f;
    [SerializeField] private int maxOrder = 1;
    int completedOrder = 0;
    private void Awake() {
        Instance = this;
    }
    private void Update() {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (waitingOrderObjects.Count >= maxOrder) return;
        fetchOrderCounter += Time.deltaTime;
        if (fetchOrderCounter > fetchOrderColdown){
            fetchOrderCounter = 0;
            RecipeSO recipe = recipelist[ UnityEngine.Random.Range(0,recipelist.Count) ];
            WaitingOrderObject waitingOrderObject = Instantiate(waitingOrderObjectTemplate);
            waitingOrderObject.SetHoldingRecipe(recipe);
            waitingOrderObject.transform.parent = this.gameObject.transform;
            waitingOrderObject.SetStartCountDown(true);
            waitingOrderObject.OnRecipeExpire += OnOrderExpire;
            waitingOrderObjects.Add(waitingOrderObject);
            OnFetchOrder?.Invoke(this,new OnOrderChangeArgs{order = waitingOrderObject});
        }
    }

    public bool CheckSubmitItem(List<KitchenSO> ingredient){
        foreach (WaitingOrderObject order in waitingOrderObjects){
            // if ingredient has the same amount of things as recipe
            if (order.GetHoldingRecipe().GetInput().Count == ingredient.Count){
                // clone ingredient for easier validating
                // if ingredient has thing that recipe require, remove it from ingredient then continue
                // else break loop
                bool isCorrectIngredient = true;
                List<KitchenSO> checkingIngredient = new(ingredient);
                foreach (KitchenSO item in order.GetHoldingRecipe().GetInput())
                {
                    if (checkingIngredient.Contains(item)){
                        checkingIngredient.Remove(item);
                    }else{
                        isCorrectIngredient = false;
                        break;
                    }
                }
                if (isCorrectIngredient){
                    OnOrderComplete?.Invoke(this,new OnOrderChangeArgs{order = order});
                    GameManager.Instance.AddBonusHeart(1);
                    waitingOrderObjects.Remove(order);
                    Debug.Log(waitingOrderObjects.Count);
                    completedOrder++;
                    return true;
                }
            }else{
                continue;
            }
        }
        
        return false;
    }

    private void OnOrderExpire(object sender, EventArgs e){
        WaitingOrderObject order = (WaitingOrderObject) sender;
        OnOrderComplete?.Invoke(this,new OnOrderChangeArgs{order = order});
        GameManager.Instance.FailOrder();
        waitingOrderObjects.Remove(order);
        Destroy(order.gameObject);
    }
}
