using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public AnimationCurve distance;
    public float speed;
    public Vector3 axis = Vector3.up;

    public Transform target;

    private float time;
    void Start () {
		
	}
    
    void Update () {
        time += Time.deltaTime;
        transform.RotateAround(target.position, axis, speed * Time.deltaTime);
        transform.position = ((transform.position - target.position).normalized * distance.Evaluate(Time.time * speed)) + target.position;
    }
}
