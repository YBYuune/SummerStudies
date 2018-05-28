using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour {

    public Vector3 buoyancy = Vector3.zero;
    public Vector3 buoyancyLimit = Vector3.zero;
    public float maxVel = 0.0f;

    void Start () {
		
	}
	
	void Update () {
		
	}
}
