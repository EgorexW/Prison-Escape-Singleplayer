#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class KeycardMigrationTool
{
    [MenuItem("Tools/Migrate Keycards")]
    public static void MigrateAllKeycards()
    {
        // Mark active scene dirty to ensure changes save
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        
        // Find and resave all Keycard prefabs
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = PrefabUtility.LoadPrefabContents(path);
            
            if (prefab.GetComponentInChildren<Keycard>() != null)
            {
                PrefabUtility.SaveAsPrefabAsset(prefab, path);
            }
            PrefabUtility.UnloadPrefabContents(prefab);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Keycard migration complete!");
    }
}
#endif