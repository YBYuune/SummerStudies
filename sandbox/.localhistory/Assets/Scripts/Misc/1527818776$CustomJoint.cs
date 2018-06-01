using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CustomJoint : MonoBehaviour {

    public bool playAnim = false;

    public AnimationCurve XMotionCurve;
    public AnimationCurve YMotionCurve;
    public AnimationCurve ZMotionCurve;

    private float currentTime = 0.0f;

    void Start ()
    {
		
	}
	

	void Update ()
    {
        if (playAnim)
        {
            currentTime += Time.deltaTime;
            transform.eulerAngles = new Vector3(XMotionCurve.Evaluate(currentTime), YMotionCurve.Evaluate(currentTime), ZMotionCurve.Evaluate(currentTime));
        }
	}
}
