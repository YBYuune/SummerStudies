using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rotator : MonoBehaviour {

    public Vector3 axis;
    public float speed;
    public bool isRotating = false;

	void Update () {
        if(isRotating)
            transform.Rotate(axis.normalized * speed);
	}
}
