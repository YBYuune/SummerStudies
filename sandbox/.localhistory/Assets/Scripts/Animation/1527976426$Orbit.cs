﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public AnimationCurve distance;
    public float speed;
    public Vector3 axis = Vector3.up;

    public Transform target;

    void Start () {
		
	}
    
    void Update () {
        transform.RotateAround(target.position, axis, speed * Time.deltaTime);
        transform.position = ((transform.position - target.position).normalized * distance.Evaluate(Time.time)) + target.position;
    }
}
