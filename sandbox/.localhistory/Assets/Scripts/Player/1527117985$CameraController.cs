using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform CamTarget;

    public float HorizontalRotationSpeed = 5.0f;
    public float VerticalRotationSpeed = 5.0f;

    [Range(0, 1)]
    public float CamFollowSpeed = .5f;

    public float Zoom;
    public float radius = 10.0f;

    public float minAngle, maxAngle;

    public bool inverseYAxis = true;

    public float jumpThreshold = 5.0f;

    private float xAngle = 0.0f; // x axis, not mouse pos
    private float yAngle = 0.0f; // y axis, not mouse pos

    private Transform PlayerTarget;

    private Vector3 posAtJump = new Vector3(-99, -99, -99);

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (CamTarget != null)
            AttachCamera(CamTarget);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyCameraCollision();

        // clamps the rotation
        xAngle += (inverseYAxis ? -1 : 1) * Input.GetAxis("Mouse Y") * VerticalRotationSpeed * Time.deltaTime;
        xAngle = Mathf.Clamp(xAngle, minAngle, maxAngle);

        yAngle += Input.GetAxis("Mouse X") * HorizontalRotationSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(xAngle, yAngle, 0.0f);

        // hold camera while jumping

        Vector3 tpos = PlayerTarget.position + Vector3.up;

        if (PlayerTarget.gameObject.GetComponent<PlayerMovement>().onFloor)
        {
            posAtJump = new Vector3(-99, -99, -99);
        }
        else
        {
            if (posAtJump.x == -99 && posAtJump.y == -99)
            {
                posAtJump = tpos;
            }
            if (tpos.y - posAtJump.y < jumpThreshold || tpos.y - posAtJump.y > -jumpThreshold)
            {
                tpos.y = posAtJump.y;
            }
        }

        // move the camera
        transform.position = Vector3.Lerp(transform.position, tpos, CamFollowSpeed);
        CamTarget.LookAt(transform);
    }

    private void ApplyCameraCollision()
    {
        // zoom based on raycast to wall
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, -(transform.position - CamTarget.position).normalized, out hit, Zoom))
        {
            if (hit.transform.gameObject.tag != "Player")
                CamTarget.localPosition = new Vector3(CamTarget.localPosition.x, CamTarget.localPosition.y, -hit.distance);
        }
        else
        {
            CamTarget.localPosition = new Vector3(CamTarget.localPosition.x, CamTarget.localPosition.y, Mathf.Lerp(CamTarget.localPosition.z, -Zoom, .1f));

        }
    }

    // used to attach a camera to the pivot
    private void AttachCamera(Transform camTransform)
    {
        camTransform.position = transform.position + Vector3.back * radius;
        camTransform.LookAt(transform);
        camTransform.parent = transform;
        PlayerTarget = transform.parent;
        transform.parent = null;
    }
}
