using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform Target;

    public float HorizontalRotationSpeed = 5.0f;
    public float VerticalRotationSpeed = 5.0f;

    public float Zoom;

    public float minAngle, maxAngle;

    private float xAngle = 0.0f;
    private float yAngle = 0.0f;

    // Use this for initialization
    void Start () {
        if(Target != null)
            AttachCamera(Target);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // clamps the rotation
        xAngle += Input.GetAxis("Mouse Y") * VerticalRotationSpeed * Time.deltaTime;
        xAngle = Mathf.Clamp(xAngle, minAngle, maxAngle);

        xAngle += Input.GetAxis("Mouse X") * VerticalRotationSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(xAngle, 0.0f, 0.0f);


        transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * HorizontalRotationSpeed);
    }

    // used to attach a camera to the pivot
    void AttachCamera(Transform camTransform)
    {
        camTransform.parent = transform;
    }
}
