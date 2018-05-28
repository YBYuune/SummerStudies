using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {
    public AnimationCurve buoyancy;
    private float currentTime = 0.0f;

    void Start () {

    }
	
	void Update () {
        currentTime += Time.deltaTime;
    }
}
