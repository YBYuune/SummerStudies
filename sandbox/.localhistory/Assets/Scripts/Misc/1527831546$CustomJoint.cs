﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CustomJoint : MonoBehaviour {

    public bool playAnim = false;
    public bool inheritTransformation = false;

    private Transform parentJoint;

    public AnimationCurve XMotionCurve;
    public AnimationCurve YMotionCurve;
    public AnimationCurve ZMotionCurve;

    public Vector3 rotationLock;

    public float animSpeed = 1.0f;
    public float animOffset = 0.0f;
    private float currentTime = 0.0f;


    void Start ()
    {
		
	}
	

	void Update ()
    {
        Vector3 rotation = Vector3.zero;
        parentJoint = transform.parent;
        if (parentJoint != null && inheritTransformation)
            rotation -= parentJoint.eulerAngles;
        if (playAnim)
        {
            currentTime += (Time.deltaTime * animSpeed);

            rotation += new Vector3(XMotionCurve.Evaluate(currentTime + animOffset),
                YMotionCurve.Evaluate(currentTime + animOffset),
                ZMotionCurve.Evaluate(currentTime + animOffset));
            if (rotationLock.x != 0.0)
            {
                rotation.x = transform.eulerAngles.x;
            }
            if (rotationLock.y != 0.0)
            {
                rotation.y = transform.eulerAngles.y;
            }
            if (rotationLock.z != 0.0)
            {
                rotation.z = transform.eulerAngles.z;
            }

        }
        transform.eulerAngles = rotation;
    }
}
