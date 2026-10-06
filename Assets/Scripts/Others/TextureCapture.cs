// AI Generated code

using System.Collections;
using System.IO;
using UnityEngine;

public class TextureCapture : MonoBehaviour
{
    [SerializeField] Vector2Int resolution = new(720, 1152);
    [SerializeField] string outputFolder = "Assets/Presentation/Posters/Captures";

    IEnumerator Start()
    {
        // Overlay canvases draw on top of everything, so only one setup in the whole scene may be active at a time,
        // including in groups that are not being captured this run
        foreach (Transform setup in transform){
            setup.gameObject.SetActive(false);
        }
        // ScreenCapture grabs the Game view as-is, so each group only captures when the Game view matches its size
        if (Screen.width != resolution.x || Screen.height != resolution.y){
            Debug.LogWarning($"{name}: skipped, Game view is {Screen.width}x{Screen.height}, needs {resolution.x}x{resolution.y}", this);
            yield break;
        }
        Directory.CreateDirectory(outputFolder);
        foreach (Transform setup in transform){
            setup.gameObject.SetActive(true);
            yield return null;
            yield return new WaitForEndOfFrame();
            Save(setup.name);
            setup.gameObject.SetActive(false);
        }
        Debug.Log($"{name}: captured {transform.childCount} textures into {outputFolder}", this);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void Save(string fileName)
    {
        var screen = ScreenCapture.CaptureScreenshotAsTexture();
        // The back buffer's alpha is not guaranteed to be 1, which would leave see-through patches in the PNG
        var opaque = new Texture2D(screen.width, screen.height, TextureFormat.RGB24, false);
        opaque.SetPixels(screen.GetPixels());
        opaque.Apply();
        File.WriteAllBytes(Path.Combine(outputFolder, fileName + ".png"), opaque.EncodeToPNG());
        Destroy(screen);
        Destroy(opaque);
    }
}
