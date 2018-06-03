using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {

    public Vector3 thrustDir = Vector3.forward;

    public Transform parent;

    public bool active = false;
	
	void FixedUpdate () {
        if (active)
        {

        }
	}
}
