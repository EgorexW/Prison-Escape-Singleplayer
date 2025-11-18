using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Locker Recipes", fileName = "Locker Recipes", order = 0)]
class LockerRecipes : ScriptableObject
{
    [SerializeField] List<LockerRecipe> lockerRecipes;
    
    public LockerRecipe GetRandomRecipe()
    {
        if (lockerRecipes == null || lockerRecipes.Count == 0){
            Debug.LogError("No locker recipes available.");
            return null;
        }
        return lockerRecipes.Random();
    }
}