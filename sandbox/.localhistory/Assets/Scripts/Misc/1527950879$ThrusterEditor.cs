using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CustomEditor(typeof(Thruster))]
public class ThrusterEditor : Editor {

    SerializedProperty direction;
    SerializedProperty speed;
    SerializedProperty affectedBody;

    /*
    public Vector3 thrustDir = Vector3.back;

    public float thrustSpeed = 1.0f;

    public Transform parent;

    public bool active = false;
     */

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

}
