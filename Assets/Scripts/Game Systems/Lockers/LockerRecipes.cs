using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Others/Locker Recipes", fileName = "Locker Recipes", order = 0)]
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

    public List<LockerRecipe> GetRandomRecipes(int length)
    {
        var recipes = lockerRecipes.Copy();
        recipes.Shuffle();
        while (recipes.Count < length){
            Debug.LogWarning("Requested more locker recipes than available. Some recipes will be duplicated.");
            recipes.AddRange(lockerRecipes);
        }
        return recipes.GetRange(0, length);
    }
}