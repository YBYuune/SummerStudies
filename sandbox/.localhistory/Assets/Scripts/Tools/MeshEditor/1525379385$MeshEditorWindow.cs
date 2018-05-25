using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshEditorWindow : EditorWindow  {
    [MenuItem("Window/MeshEditor")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MeshEditorWindow),false,"Mesh Edit");
    }

    private void OnGUI()
    {
        
    }

}
