using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIKJoint : MonoBehaviour {

    public Vector3 Axis;
    public CustomIKJoint[] Joints;
    public Vector3 StartOffset;

	void Awake()
    {
        StartOffset = transform.localPosition;
    }

    public Vector3 ForwardKinematics(float[] angles)
    {
        Vector3 prevPoint = Joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
    }

}
