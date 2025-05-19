using UnityEditor;
using UnityEngine;
using Editor = UnityEditor.Editor;

[CustomEditor(typeof(FireTrap))]
public class AreaEditor : Editor
{
    private void OnSceneGUI()
    {
        FireTrap area = (FireTrap)target;

        // Create a resizable handle in the Scene View
        EditorGUI.BeginChangeCheck();
        Vector3 newSize = Handles.ScaleHandle(area.areaSize, area.transform.position, Quaternion.identity, 1f);
        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(area, "Resize Area Effect");
            area.areaSize = newSize;
        }

        // Display the box or sphere with the resizable handle
        Handles.color = new Color(0, 1, 0, 1f); // Transparent green
        Handles.DrawWireCube(area.transform.position, area.areaSize); // Draw a box
        // Handles.DrawWireSphere(area.transform.position, area.areaSize.x / 2); // Draw a sphere (optional)
    }
}