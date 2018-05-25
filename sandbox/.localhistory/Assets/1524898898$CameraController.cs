using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform Target;

    public float HorizontalRotationSpeed = 5.0f;
    public float VerticalRotationSpeed = 5.0f;

    public float Zoom;

    [Range(0, 360)]
    public float xzAngle;

    [Range(0, 360)]
    public float yAngle;


    // Use this for initialization
    void Start () {
        HorizontalRotationSpeed = (HorizontalRotationSpeed * 2.0f) / 10.0f;
        VerticalRotationSpeed   = (VerticalRotationSpeed   * 2.0f) / 10.0f;
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Target.position,Vector3.up,xzAngle)
	}
}
