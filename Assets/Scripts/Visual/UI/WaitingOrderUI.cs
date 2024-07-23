using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingOrderUI : MonoBehaviour
{
    [SerializeField] private OrderIconUI orderIconUITemplate;
    private OrderManager orderManager;
    private List<OrderIconUI> orderIconUIInstances = new();
    private Vector2 orderSpacing = new Vector2(0,-100);
    private void Start() {
        orderManager = OrderManager.Instance;
        orderManager.OnFetchOrder += OnAddOrder;
        orderManager.OnOrderComplete += OnCompleteOrder;
    }
    private void OnAddOrder(object sender,OrderManager.OnOrderChangeArgs args){
        OrderIconUI inst = Instantiate(orderIconUITemplate,transform);
        inst.AddRecipe(args.order.GetHoldingRecipe());
        inst.transform.localPosition = orderSpacing * orderIconUIInstances.Count;
        inst.gameObject.SetActive(true);
        inst.refObject = args.order;
        orderIconUIInstances.Add(inst);
    }
    private void OnCompleteOrder(object sender,OrderManager.OnOrderChangeArgs args){
        foreach (OrderIconUI iconInst in orderIconUIInstances){
            if (iconInst.refObject == args.order){
                orderIconUIInstances.Remove(iconInst);
                Destroy(iconInst.gameObject);
                UpdateIconPosition();
                break;
            }
        }
    }

    private void UpdateIconPosition(){
        int i = 0;
        foreach (OrderIconUI icon in orderIconUIInstances){
            icon.gameObject.transform.localPosition = orderSpacing * i;
            i++;
        }
    }
}
