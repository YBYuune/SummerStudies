﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public AnimationCurve distance;
    public float orbitSpeed;
    public float distSpeed;
    public Vector3 axis = Vector3.up;
    public Transform target;

    private float time;
    void Start () {
		
	}
    
    void Update () {
        time += Time.deltaTime;
        transform.position = ((transform.position - target.position).normalized * distance.Evaluate(time * distSpeed)) + target.position;
        transform.RotateAround(target.position, axis, orbitSpeed * Time.deltaTime);
    }
}
