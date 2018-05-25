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

        //float angleDistance = m_FollowObject->m_RotRad - +90.f;
        //float angleInRadians = (angleDistance) * (PI / 180.f);
        //float radius = 15.0f;
        //vec3 offset = vec3(cosf(angleInRadians), 0, sinf(angleInRadians)) * radius;
        //vec3 newPos = vec3(m_FollowObject->GetPosition().x, m_FollowObject->GetPosition().y + 1.0f, m_FollowObject->GetPosition().z);

        Vector3 offset = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad*angle)) * Zoom;
        transform.position = Target.position + offset;
        transform.LookAt(Target);
	}
}
