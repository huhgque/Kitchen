using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject parent;
    [SerializeField] private GameObject iconTemplate;
    private List<GameObject> iconList = new();
    private void Start() {
        parent.OnIngredientAdded += OnIngredientAdded;
    }
    private void OnIngredientAdded(object sender,PlateKitchenObject.OnIngredientAddedArgs args){
        GameObject newIcon = Instantiate(iconTemplate,transform);
        newIcon.SetActive(true);
        newIcon.GetComponent<IconUI>()?.TryAddIcon(args.ingredient);
        iconList.Append(newIcon);
    }
}
