using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DirectionalIntensity : MonoBehaviour {

    private Light childDirLight;

	// Use this for initialization
	void Start () {
        childDirLight = GetComponentInChildren<Light>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
