﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIKSolver : MonoBehaviour {

    public string _name;
    [Space(25)]
    public CustomIKJoint[] Joints;

    public CustomIKJoint Ankle;
    public Transform target;

    public float SampleDist;
    public float LearningRate;
    public float Threshold;

    private void Update()
    {
        float[] angles = new float[Joints.Length];

        for (int i = 0; i < Joints.Length; i++)
        {
            angles[i] = Joints[i].rotation;
        }
        InverseKinematics(target.position, angles);

        for (int i = 0; i < Joints.Length; i++)
        {
                Joints[i].transform.rotation = Quaternion.Slerp (Joints[i].transform.rotation,Quaternion.AngleAxis(angles[i] - Joints[i].AngleOffset,Joints[i].Axis),.8f);
        } 
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

            //angles[i] = Mathf.Clamp(angles[i], Joints[i].MinAngle, Joints[i].MaxAngle);

            if (DistanceFromTarget(target, angles) < Threshold)
                return;

        }
    }

    public void SolveIK(Vector3 target)
    {

    }


}
