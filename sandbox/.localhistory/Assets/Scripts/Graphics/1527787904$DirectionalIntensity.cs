using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DirectionalIntensity : MonoBehaviour {

    private Light childDirLight;

	// Use this for initialization
	void Awake () {
        childDirLight = GetComponentInChildren<Light>();

    }
	
	// Update is called once per frame
	void Update () {
        childDirLight.intensity = 1.0f - GetComponent<Light>().intensity;

    }
}
