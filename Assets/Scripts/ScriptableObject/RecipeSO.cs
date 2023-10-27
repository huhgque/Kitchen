using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe",menuName = "SO/Recipe",order =1)]
public class RecipeSO : ScriptableObject
{
    [SerializeField] private KitchenSO[] input ;
    [SerializeField] private KitchenSO output ;
    [SerializeField] private int processStep ;
    public List<KitchenSO> GetInput(){
        return input.ToList();
    }
    public KitchenSO GetOutput(){
        return output;
    }
    public int GetProcessStep(){
        return processStep;
    }
}
