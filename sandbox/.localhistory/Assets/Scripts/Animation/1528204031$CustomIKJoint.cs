using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIKJoint : MonoBehaviour {

    public Vector3 Axis;

    public float MinAngle;
    public float MaxAngle;
    [HideInInspector] public Vector3 StartOffset;

    void Awake()
    {
        StartOffset = transform.localPosition;
        Debug.Log(transform.position);
    }
}
