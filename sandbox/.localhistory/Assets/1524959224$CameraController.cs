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
        Target.parent = transform;
        //Target.parent.GetComponent<PlayerMovement>().cameraPos = transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Target.RotateAround(Target.position, Vector3.up, Input.GetAxis("Mouse X") * HorizontalRotationSpeed);
        Target.RotateAround(Target.position, Target.right, -Input.GetAxis("Mouse Y") * VerticalRotationSpeed);
    }

    // used to attach a camera to the pivot
    void AttachCamera(Transform camTransform)
    {
        camTransform.parent = transform;
    }
}
