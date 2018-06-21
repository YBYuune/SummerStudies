using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCurves : MonoBehaviour {

    public AnimationCurve XAxisCurve;
    public AnimationCurve YAxisCurve;
    public AnimationCurve ZAxisCurve;

    [Space(25)] public float TimeOffset;

    private Vector3 pos;
    // Use this for initialization
    void Start () {
		pos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = pos;

        newPos.x += XAxisCurve.Evaluate(Time.time);
        newPos.y += YAxisCurve.Evaluate(Time.time);

        transform.position = newPos;
    }
}
