using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Keycard Recipes", fileName = "Keycard Recipes", order = 0)]
class KeycardRecipes : ScriptableObject
{
    [BoxGroup("References")][Required][SerializeField] GameObject baseKeycardPrefab;
    [BoxGroup("References")][Required][SerializeField] AccessLevel defaultAccessLevel;
    
    [SerializeField] List<KeycardRecipe> recipes;
    
    public Keycard CreateAndGetResult(Keycard keycard1, Keycard keycard2)
    {
        foreach (var recipe in recipes){
            var option1Keycard1Match =
                recipe.options1.Find(match => keycard1.accessLevel.GetAllAccessLevels().Contains(match)) != null;
            var option2Keycard2Match =
                recipe.options2.Find(match => keycard2.accessLevel.GetAllAccessLevels().Contains(match)) != null;
            var option1Keycard2Match =
                recipe.options1.Find(match => keycard2.accessLevel.GetAllAccessLevels().Contains(match)) != null;
            var option2Keycard1Match =
                recipe.options2.Find(match => keycard1.accessLevel.GetAllAccessLevels().Contains(match)) != null;
            // Debug.Log("Recipe: " + recipe.result + " Matches: " +
            //           option1Keycard1Match + "," + option2Keycard2Match + "," +
            //           option1Keycard2Match + "," + option2Keycard1Match);
            if (option1Keycard1Match && option2Keycard2Match ||
                option1Keycard2Match && option2Keycard1Match){
                var oneUse = keycard1.oneUse || keycard2.oneUse || recipe.oneUse;
                return CreateKeycard(recipe.result, oneUse);
            }
        }
        Debug.LogWarning("No recipe found for keycards: " + keycard1.accessLevel + " + " + keycard2.accessLevel);
        return CreateKeycard(defaultAccessLevel, false);
    }

    Keycard CreateKeycard(AccessLevel accessLevel, bool oneUse)
    {
        var result = Instantiate(baseKeycardPrefab).GetComponent<Keycard>();
        result.accessLevel = accessLevel;
        result.oneUse = oneUse;
        return result;
    }
}

[Serializable]
class KeycardRecipe
{
    public List<AccessLevel> options1;
    public List<AccessLevel> options2;
    [Required] public AccessLevel result;
    public bool oneUse = false;
}