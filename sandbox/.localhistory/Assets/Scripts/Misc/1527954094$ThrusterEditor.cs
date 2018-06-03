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

    void OnEnable()
    {
        direction = serializedObject.FindProperty("thrustDir");
        speed = serializedObject.FindProperty("thrustSpeed");
        affectedBody = serializedObject.FindProperty("parent");
        active = serializedObject.FindProperty("active");
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

    protected virtual void OnSceneGUI()
    {
        Thruster t = target as Thruster;
        Handles.color = Color.magenta;
        float size = HandleUtility.GetHandleSize(t.transform.position) * 5.0f;
        Handles.ScaleValueHandle(1.0f, t.transform.position,  Quaternion.LookRotation(t.transform.TransformDirection(t.thrustDir), t.transform.up), size, Handles.ArrowHandleCap, 0.0f);
    }

}
