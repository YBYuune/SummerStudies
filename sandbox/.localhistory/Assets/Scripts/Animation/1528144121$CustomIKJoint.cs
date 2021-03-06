﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIKJoint : MonoBehaviour {

    public Vector3 Axis;
    public CustomIKJoint[] Joints;
    [HideInInspector]public Vector3 StartOffset;

	void Awake()
    {
        StartOffset = transform.localPosition;
    }

    public Vector3 ForwardKinematics(float[] angles)
    {
        Vector3 prevPoint = Joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 0; i < Joints.Length; ++i)
        {
            rotation *= Quaternion.AngleAxis(angles[i], Joints[i].Axis);
            prevPoint = prevPoint + rotation * Joints[i].StartOffset;
        }

        return prevPoint;
    }

}
