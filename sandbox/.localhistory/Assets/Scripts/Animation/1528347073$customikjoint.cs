using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIKJoint : MonoBehaviour {

    public Vector3 Axis;

    public float MinAngle;
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

    void OnDrawGizmos()
    {
        if (Visualise)
        {
            //if(!Application.isPlaying) {
            //	OnRenderObject();
            //}
            if (Ankle == null || Joint == null || Normal == Vector3.zero)
            {
                return;
            }
            if (!Application.isPlaying)
            {
                ComputeNormal();
            }
            UltiDraw.Begin();
            UltiDraw.DrawSphere(GetPivotPosition(), Quaternion.identity, 0.025f, UltiDraw.Cyan.Transparent(0.5f));
            UltiDraw.DrawArrow(GetPivotPosition(), GetPivotPosition() + 0.25f * (GetPivotRotation() * Normal.normalized), 0.8f, 0.02f, 0.1f, UltiDraw.Cyan.Transparent(0.5f));
            UltiDraw.DrawSphere(GetPivotPosition(), Quaternion.identity, Radius, UltiDraw.Mustard.Transparent(0.5f));
            UltiDraw.End();
        }
    }
}
