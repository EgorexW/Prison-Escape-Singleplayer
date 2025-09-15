using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

// AI Generated Code
// This script creates a Sound asset from selected AudioClip(s) in the Unity Editor.

public class SoundCreator
{
    [MenuItem("Assets/Create/Sound from AudioClip", true)]
    private static bool ValidateCreateSound()
    {
        return Selection.objects.All(obj => obj is AudioClip) && Selection.objects.Length > 0;
    }

    [MenuItem("Assets/Create/Sound from AudioClip")]
    private static void CreateSound()
    {
        AudioClip[] clips = Selection.objects.OfType<AudioClip>().ToArray();
        if (clips.Length == 0)
        {
            Debug.LogError("Selected object(s) are not AudioClip(s).");
            return;
        }

        // Create the Sound asset
        Sound soundAsset = ScriptableObject.CreateInstance<Sound>();
        soundAsset.clips = new List<AudioClip>(clips);

        // Generate path for the new asset
        string newPath;
        
        string path = AssetDatabase.GetAssetPath(clips[0]);
        newPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), clips[0].name + ".asset");

        AssetDatabase.CreateAsset(soundAsset, newPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = soundAsset;
    }
}