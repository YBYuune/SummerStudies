using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        Handles.ScaleValueHandle(t.thrustDir, t.transform.position, Quaternion.identity, size, Handles.ArrowHandleCap, snap);
    }

}
