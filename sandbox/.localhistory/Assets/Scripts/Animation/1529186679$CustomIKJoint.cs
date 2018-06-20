using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIKJoint : MonoBehaviour {

    public Vector3 Axis;

    public float MinAngle; // unused for now, hope to use soon
    public float MaxAngle;

    public float AngleOffset;

    public bool Visualise;


    [HideInInspector] public Vector3 StartOffset;
    [HideInInspector] public float rotation;

    void Awake()
    {
        StartOffset = transform.localPosition;
    }

    void Update()
    {

        rotation = Vector3.Dot(Axis, transform.localEulerAngles) + AngleOffset;
        Debug.Log(name + " : " + transform.position);
    }

    void OnDrawGizmos() // visualise the joint location
    {
        if (Visualise)
        {
            UltiDraw.Begin();
            UltiDraw.DrawSphere(transform.position, Quaternion.identity, 0.25f, UltiDraw.Purple.Transparent(0.5f));

            UltiDraw.DrawArrow(transform.position, transform.position + 0.25f * (transform.rotation * Axis), 0.8f, 0.02f, 0.1f, UltiDraw.Cyan.Transparent(0.5f));
            UltiDraw.End();
        }
    }
}
