using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public AnimationCurve distance;
    public float speed;
    public Vector3 axis = Vector3.up;

    public Vector3 target;

    void Start () {
		
	}
    
    void Update () {
		transform.RotateAround(target, axis,speed * Time.deltaTime)
	}
}
