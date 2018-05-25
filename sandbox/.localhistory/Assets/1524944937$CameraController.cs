using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform Target;

    public float HorizontalRotationSpeed = 5.0f;
    public float VerticalRotationSpeed = 5.0f;

    public float Zoom;


    // Use this for initialization
    void Start () {
        transform.parent = Target;
        Target.parent
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(Target.position, Vector3.up, Input.GetAxis("Mouse X") * HorizontalRotationSpeed);
        transform.RotateAround(Target.position, transform.right, -Input.GetAxis("Mouse Y") * VerticalRotationSpeed);
    }
}
