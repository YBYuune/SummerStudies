using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DirectionalIntensity : MonoBehaviour {

    private Light childDirLight;

	// Use this for initialization
	void Awake () {
        childDirLight = transform.GetChild(0).GetComponent<Light>();

    }
	
	// Update is called once per frame
	void Update () {

    }
}
