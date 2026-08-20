using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Others/Keycard Recipes", fileName = "Keycard Recipes", order = 0)]
class KeycardRecipes : ScriptableObject
{
    [BoxGroup("References")][Required][SerializeField] GameObject baseKeycardPrefab;
    [BoxGroup("References")][Required][SerializeField] GameObject defaultPrefab;
    
    [SerializeField] List<KeycardRecipe> recipes;
    
    public Keycard CreateAndGetResult(Keycard keycard1, Keycard keycard2)
    {
        foreach (var recipe in recipes){
            var option1Keycard1Match =
                recipe.options1.Find(match => keycard1.AccessLevel.GetAllAccessLevels().Contains(match)) != null;
            var option2Keycard2Match =
                recipe.options2.Find(match => keycard2.AccessLevel.GetAllAccessLevels().Contains(match)) != null;
            var option1Keycard2Match =
                recipe.options1.Find(match => keycard2.AccessLevel.GetAllAccessLevels().Contains(match)) != null;
            var option2Keycard1Match =
                recipe.options2.Find(match => keycard1.AccessLevel.GetAllAccessLevels().Contains(match)) != null;
            // Debug.Log("Recipe: " + recipe.result + " Matches: " +
            //           option1Keycard1Match + "," + option2Keycard2Match + "," +
            //           option1Keycard2Match + "," + option2Keycard1Match);
            if (option1Keycard1Match && option2Keycard2Match ||
                option1Keycard2Match && option2Keycard1Match){
                var status = KeycardStatus.Permanent;
                if (keycard1.Status == KeycardStatus.UseActive || keycard2.Status == KeycardStatus.UseActive || recipe.status == KeycardStatus.UseActive){
                    status = KeycardStatus.UseActive;
                }
                if (keycard1.Status == KeycardStatus.UseInactive || keycard2.Status == KeycardStatus.UseInactive || recipe.status == KeycardStatus.UseInactive){
                    status = KeycardStatus.UseInactive;
                }
                return CreateKeycard(recipe.result, status);
            }
        }
        Debug.LogWarning("No recipe found for keycards: " + keycard1.AccessLevel + " + " + keycard2.AccessLevel);
        return CreateDefault();
    }

    Keycard CreateDefault()
    {
        var result = Instantiate(defaultPrefab).GetComponent<Keycard>();
        return result;
    }

    Keycard CreateKeycard(AccessLevel accessLevel, KeycardStatus status)
    {
        var result = Instantiate(baseKeycardPrefab).GetComponent<Keycard>();
        result.SetAccessLevel(accessLevel);
        result.SetStatus(status);
        return result;
    }
}

[Serializable]
class KeycardRecipe
{
    public List<AccessLevel> options1;
    public List<AccessLevel> options2;
    [Required] public AccessLevel result;
    public KeycardStatus status = KeycardStatus.Permanent;
}