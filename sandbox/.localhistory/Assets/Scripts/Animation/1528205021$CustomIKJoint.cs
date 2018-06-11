using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIKJoint : MonoBehaviour {

    public Vector3 Axis;

    public float MinAngle;
    public float MaxAngle;

    public float AngleOffset;


    [HideInInspector] public Vector3 StartOffset;
    [HideInInspector] public float rotation;

    void Awake()
    {
        StartOffset = transform.localPosition;
        rotation = Vector3.Dot(Axis, transform.rotation.eulerAngles) + AngleOffset;
    }

    void Update()
    {

        Debug.Log(name + " : " + transform.position);
    }
}
