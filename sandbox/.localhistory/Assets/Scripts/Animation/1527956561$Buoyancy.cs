using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour {
    public AnimationCurve buoyancy;
    public float lengthInSeconds = 1.0f;
    public bool randomOffset = false;
    public float offset = 0.0f;
    private float currentTime = 0.0f;

    private float startY;
    private Rigidbody body;

    void Start () {
        startY = transform.position.y;
    }
	
	void Update () {
        currentTime += Time.deltaTime;

        body = GetComponent<Rigidbody>();
        Vector3 vel = body.velocity;
        vel.y = 0.0f;//buoyancy.Evaluate(currentTime / lengthInSeconds);
        body.velocity = vel;

        Vector3 pos = transform.position;
        pos.y = startY + buoyancy.Evaluate((currentTime / lengthInSeconds) + offset);
        transform.position = pos;
    }
}
