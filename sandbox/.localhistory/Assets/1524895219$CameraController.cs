using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform Target;

    public float HorizontalRotationSpeed = 5.0f;
    public float VerticalRotationSpeed   = 5.0f;

    public float Zoom;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = new Vector3();
        transform.position = Target.position + offset;
	}
}
