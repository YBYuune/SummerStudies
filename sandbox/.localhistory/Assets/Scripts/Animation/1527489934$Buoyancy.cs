using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {
    public AnimationCurve buoyancy;
    public float lengthInSeconds = 1.0f;
    private float currentTime = 0.0f;

    void Start () {

    }
	
	void Update () {
        currentTime += Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y = buoyancy.Evaluate(currentTime /= lengthInSeconds);
        transform.position = pos;
    }
}
