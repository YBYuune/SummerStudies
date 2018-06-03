using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Thruster))]
public class ThrusterEditor : Editor {

    SerializedProperty direction;
    SerializedProperty speed;
    SerializedProperty affectedBody;
    SerializedProperty active;
    float i = 0.0f;

    void OnEnable()
    {
        direction = serializedObject.FindProperty("thrustDir");
        speed = serializedObject.FindProperty("thrustSpeed");
        affectedBody = serializedObject.FindProperty("parent");
        active = serializedObject.FindProperty("active");
    }

    void OnFocus()
    {
        // Remove delegate listener if it has previously
        // been assigned.
        // Add (or re-add) the delegate.
    }

    public override void OnInspectorGUI()
    {
        Thruster t = target as Thruster;
        serializedObject.Update();
        EditorGUILayout.PropertyField(direction);
        EditorGUILayout.LabelField(t.transform.TransformDirection(t.thrustDir).ToString());

        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(affectedBody);
        EditorGUILayout.PropertyField(active);
        serializedObject.ApplyModifiedProperties();

    }

}
