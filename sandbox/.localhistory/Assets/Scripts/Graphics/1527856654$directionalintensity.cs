using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DirectionalIntensity : MonoBehaviour {

    private Light childDirLight;
    [Range(0,1)]public float overallIntensity = 1.0f;

    private float prevIntensity;

	// Update is called once per frame
	void LateUpdate() {



        childDirLight = transform.GetChild(0).GetComponent<Light>();
        childDirLight.intensity = (1.0f - GetComponent<Light>().intensity);
        childDirLight.color = GetComponent<Light>().color;
        prevIntensity = overallIntensity;
    }
}
