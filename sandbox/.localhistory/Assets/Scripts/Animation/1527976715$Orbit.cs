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
        //transform.RotateAround(target.position, axis, speed * Time.deltaTime);
        //transform.position = ((transform.position - target.position).normalized * distance.Evaluate(time * speed)) + target.position;

        transform.RotateAround(target.position, axis, speed * Time.deltaTime);
        var desiredPosition = (transform.position - target.position).normalized * distance.Evaluate(time * speed) + target.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 0.1f);
    }
}
