﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Transform Target;

    float HorizontalRotationSpeed;
    float VerticalRotationSpeed;

    float Zoom;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3(0,0,0));
	}
}
