using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {

    public Vector3 thrustDir = Vector3.back;

    public float thrustSpeed = 1.0f;

    public Transform parent;

    public bool active = false;

    private void Awake()
    {
        if (parent == null)
            parent = transform.parent;
    }

    void FixedUpdate () {
        if (active)
        {
            parent.GetComponent<Rigidbody>().AddForceAtPosition(thrustDir.normalized * thrustSpeed, transform.position);
        }
	}

    void OnDrawGizmos()
    {
        Thruster t = target as Thruster;
        Handles.color = Color.cyan;
        float size = HandleUtility.GetHandleSize(t.transform.position) * 5.0f;
        Handles.ScaleValueHandle(1.0f, t.transform.position, Quaternion.LookRotation(t.transform.TransformDirection(t.thrustDir), t.transform.up), size, Handles.ArrowHandleCap, 0.0f);
    }
}
