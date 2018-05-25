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
        if(Target != null)
            AttachCamera(Target);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // clamps the rotation
        yRotate += Input.GetAxis("Mouse Y") * RotationSensitivity * Time.deltaTime;
        yRotate = Mathf.Clamp(yRotate, minAngle, maxAngle);
        transform.eulerAngles = new Vector3(yRotate, 0.0f, 0.0f);

        transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * HorizontalRotationSpeed);
        transform.RotateAround(transform.position, Target.right, -Input.GetAxis("Mouse Y") * VerticalRotationSpeed);
    }

    // used to attach a camera to the pivot
    void AttachCamera(Transform camTransform)
    {
        camTransform.parent = transform;
    }
}
