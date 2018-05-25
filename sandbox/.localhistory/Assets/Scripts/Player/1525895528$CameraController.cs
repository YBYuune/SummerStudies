using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform Target;

    public float HorizontalRotationSpeed = 5.0f;
    public float VerticalRotationSpeed = 5.0f;

    public float Zoom;
    public float radius = 10.0f;

    public float minAngle, maxAngle;

    public bool inverseYAxis = true;

    public LayerMask layermask;

    private float xAngle = 0.0f; // x axis, not mouse pos
    private float yAngle = 0.0f; // y axis, not mouse pos


    // Use this for initialization
    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (Target != null)
            AttachCamera(Target);
    }
	
	// Update is called once per frame
	void Update ()
    {
        camTransform.eulerAngles.Set(0, 0, 0);
        // zoom based on raycast to wall
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, -(transform.position - Target.position).normalized, out hit, Zoom, layermask))
        {
            Target.localPosition = new Vector3(Target.localPosition.x, Target.localPosition.y, Mathf.Lerp(Target.localPosition.z, -hit.distance + 2, .1f) );
            print(hit.distance);
        }
        else
        {
            Target.localPosition = new Vector3(Target.localPosition.x, Target.localPosition.y, Mathf.Lerp(Target.localPosition.z, -Zoom,.95f));

        }

        // clamps the rotation
        xAngle += (inverseYAxis ? -1 : 1) * Input.GetAxis("Mouse Y") * VerticalRotationSpeed * Time.deltaTime;
        xAngle = Mathf.Clamp(xAngle, minAngle, maxAngle);

        yAngle += Input.GetAxis("Mouse X") * HorizontalRotationSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(xAngle, yAngle, 0.0f);
    }

    // used to attach a camera to the pivot
    void AttachCamera(Transform camTransform)
    {
        camTransform.position = transform.position + Vector3.back * radius;
        camTransform.parent = transform;
    }
}
