using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour {
    public AnimationCurve buoyancy;
    public float lengthInSeconds = 1.0f;
    private float currentTime = 0.0f;

    private float startY;
    private Rigidbody body;

    void Start () {
        startY = transform.position.y;
    }
	
	void Update () {
        currentTime += Time.deltaTime;

        Vector3 vel = transform.position;
        vel.y = buoyancy.Evaluate(currentTime / lengthInSeconds);
        transform.position = pos;
    }
}
