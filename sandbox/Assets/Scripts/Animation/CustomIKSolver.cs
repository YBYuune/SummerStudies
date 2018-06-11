using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIKSolver : MonoBehaviour {

    public string _name; // this is just a label to tell what solver this is if you have multiple on the same game object
    [Space(25)]
    public CustomIKJoint[] Joints; // list of joints that are part of the calculation for IK

    public CustomIKJoint Ankle; // foot end effector location
    public Transform Target; // where the IK is pointing at

    public Vector3 TargetOffset = new Vector3(0f, 0f, 0f); // some offset stuff 
    public float TargetMultiplier = 1f;                     // some offset stuff 

    public int Itterations = 3; // how many times the IK is calculated per frame

    private void LateUpdate()
    {
        SolveIK(Target.position);
    }

    public void SolveIK(Vector3 target)
    {
        for (int k = 0; k < Itterations; k++)
        {
            for (int i = 0; i < Joints.Length; i++)
            {
                Joints[i].transform.rotation = Quaternion.Slerp(Joints[i].transform.rotation,
                    Quaternion.FromToRotation(GetPivot() - Joints[i].transform.position, ((target + TargetOffset) * TargetMultiplier) - Joints[i].transform.position) * Joints[i].transform.rotation,
                    (float)(i + 1) / (float)Joints.Length);
            }
        }
    }

    public Vector3 GetPivot()
    {
        return Ankle.transform.position + Ankle.transform.rotation * Ankle.StartOffset;
    }

    // first method I tried out using Alan zucconis tutorial

    //public Vector3 ForwardKinematics(float[] angles)
    //{
    //    Vector3 prevPoint = Joints[0].transform.position;
    //    Quaternion rotation = Quaternion.identity;
    //    for (int i = 0; i < Joints.Length; ++i)
    //    {
    //        rotation *= Quaternion.AngleAxis(angles[i], Joints[i].Axis);
    //        prevPoint = prevPoint + rotation * Joints[i].StartOffset;
    //    }

    //    return prevPoint;
    //}

    //public float DistanceFromTarget(Vector3 target, float[] angles)
    //{
    //    Vector3 point = ForwardKinematics(angles);
    //    return Vector3.Distance(point, target);
    //}

    //public float PartialGradient(Vector3 target, float[] angles, int i)
    //{
    //    float angle = angles[i];

    //    float f_x = DistanceFromTarget(target, angles);
    //    angles[i] += SampleDist;
    //    float f_x_plus_d = DistanceFromTarget(target, angles);
    //    float gradient = (f_x_plus_d - f_x) / SampleDist;

    //    angles[i] = angle;
    //    return gradient;
    //}

    //public void InverseKinematics(Vector3 target, float[] angles)
    //{
    //    if (DistanceFromTarget(target, angles) < Threshold)
    //        return;

    //    for (int i = 0; i < Joints.Length; ++i)
    //    {
    //        float gradient = PartialGradient(target, angles, i);
    //        angles[i] -= LearningRate * gradient;

    //        //angles[i] = Mathf.Clamp(angles[i], Joints[i].MinAngle, Joints[i].MaxAngle);

    //        if (DistanceFromTarget(target, angles) < Threshold)
    //            return;

    //    }
    //}


}
