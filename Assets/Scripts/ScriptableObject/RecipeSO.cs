using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe",menuName = "SO/Recipe",order =1)]
public class RecipeSO : ScriptableObject
{
    [SerializeField] private KitchenSO[] input ;
    [SerializeField] private KitchenSO output ;
    [SerializeField] private int processStep ;
    public KitchenSO[] GetInput(){
        return input;
    }
    public KitchenSO GetOutput(){
        return output;
    }
    public int GetProcessStep(){
        return processStep;
    }
}
