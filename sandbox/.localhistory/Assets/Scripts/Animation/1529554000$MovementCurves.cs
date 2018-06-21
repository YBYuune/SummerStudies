using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCurves : MonoBehaviour {

    public AnimationCurve XAxisCurve;
    public AnimationCurve YAxisCurve;
    public AnimationCurve ZAxisCurve;

    [Space(25)] public float TimeOffset;

    public float YOffset;

    private Vector3 pos;
    // Use this for initialization
    void Start () {
		pos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = pos;

        newPos.x = XAxisCurve.Evaluate(Time.time + TimeOffset);
        newPos.y = YAxisCurve.Evaluate(Time.time + TimeOffset) + YOffset;

        transform.localPosition = newPos;
    }
}
