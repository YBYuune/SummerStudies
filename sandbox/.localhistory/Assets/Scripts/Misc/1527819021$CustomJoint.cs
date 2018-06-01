using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CustomJoint : MonoBehaviour {

    public bool playAnim = false;
    public bool inheritTransformation = false;

    private Transform parentJoint;

    public AnimationCurve XMotionCurve;
    public AnimationCurve YMotionCurve;
    public AnimationCurve ZMotionCurve;

    private float currentTime = 0.0f;

    void Start ()
    {
		
	}
	

	void Update ()
    {
        parentJoint = transform.parent;
        if (parentJoint != null && inheritTransformation)
            rotation -= parentJoint.eulerAngles;
        if (playAnim)
        {
            currentTime += Time.deltaTime;

            Vector3 rotation = new Vector3(XMotionCurve.Evaluate(currentTime), YMotionCurve.Evaluate(currentTime), ZMotionCurve.Evaluate(currentTime));

            transform.eulerAngles = rotation;
        }
	}
}
