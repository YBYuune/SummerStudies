using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIKJoint : MonoBehaviour {

    public Vector3 Axis;
    public CustomIKJoint[] Joints;
    public float SampleDist;
    public float LearningRate;
    public float Threshold;
    [HideInInspector] public Vector3 StartOffset;

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

    public float DistanceFromTarget(Vector3 target, float[] angles)
    {
        Vector3 point = ForwardKinematics(angles);
        return Vector3.Distance(point, target);
    }

    public float PartialGradient(Vector3 target, float[] angles, int i)
    {
        float angle = angles[i];

        float f_x = DistanceFromTarget(target, angles);
        angles[i] += SampleDist;
        float f_x_plus_d = DistanceFromTarget(target, angles);
        float gradient = (f_x_plus_d - f_x) / SampleDist;

        angles[i] = angle;
        return gradient;
    }

    public void InverseKinematics(Vector3 target, float[] angles)
    {
        if (DistanceFromTarget(target, angles) < Threshold)
            return;

        for (int i = 0; i < Joints.Length; ++i)
        {
            float gradient = PartialGradient(target, angles, i);
            angles[i] -= LearningRate * gradient;

            if (DistanceFromTarget(target, angles) < Threshold)
                return;

        }
    }

}
