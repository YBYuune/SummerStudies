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
            

        }
        transform.localEulerAngles = rotation;
        Vector3 localAngles = transform.localEulerAngles;
        //if (rotationLock.x != 0.0)
        //{
        //    localAngles.x = 0.0f;
        //}
        //if (rotationLock.y != 0.0)
        //{
        //    localAngles.y = 0.0f;
        //}
        //if (rotationLock.z != 0.0)
        //{
        //    localAngles.z = 0.0f;
        //}
        transform.localEulerAngles = localAngles;
    }

    void OnDrawGizmos()
    {

        UltiDraw.Begin();
        //
        UltiDraw.DrawBone(
            transform.position, 
            Quaternion.FromToRotation(-transform.parent.forward, transform.position - transform.parent.position) * transform.parent.rotation,
            0.5f,
            (transform.position - transform.parent.position).magnitude, UltiDraw.IndianRed);
        //
        UltiDraw.End();
    }

}
