using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    public bool TryAddIcon(KitchenSO kitchenSO){
        icon.sprite = kitchenSO.sprite;
        return true;
    }
}
