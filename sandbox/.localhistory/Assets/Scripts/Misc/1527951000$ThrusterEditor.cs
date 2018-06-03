using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CustomEditor(typeof(Thruster))]
public class ThrusterEditor : Editor {

    SerializedProperty direction;
    SerializedProperty speed;
    SerializedProperty affectedBody;
    SerializedProperty active;

    /*
    public Vector3 thrustDir = Vector3.back;

    public float thrustSpeed = 1.0f;

    public Transform parent;

    public bool active = false;
     */

    void OnEnable()
    {
        direction = serializedObject.FindProperty("thrustDir");
        speed = serializedObject.FindProperty("thrustSpeed");
        affectedBody = serializedObject.FindProperty("parent");
        active = serializedObject.FindProperty("active");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

}
