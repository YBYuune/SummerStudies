using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshEditorWindow : EditorWindow  {
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    [MenuItem("Window/MeshEditor")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MeshEditorWindow),false,"Mesh Edit");
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }

}
