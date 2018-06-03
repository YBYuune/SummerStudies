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
        serializedObject.Update();
        EditorGUILayout.PropertyField(direction);
        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(affectedBody);
        EditorGUILayout.PropertyField(active);
        serializedObject.ApplyModifiedProperties();
    }

    protected virtual void OnSceneGUI()
    {
        Thruster t = target as Thruster;
        float size = HandleUtility.GetHandleSize(t.transform.position) * 5.0f;
        Handles.ScaleValueHandle(1.0f, t.transform.position,  Quaternion.AngleAxis(0.0f, t.transform.InverseTransformDirection(t.thrustDir)), size, Handles.ArrowHandleCap, 0.0f);
    }

}
