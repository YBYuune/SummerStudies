using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Transform cameraPivot;

    public Transform MeshTransform;

    public float MaxVelocity;
    public float Speed;
    public float JumpSpeed;

    public float movementAngle = 16.0f;
    public float movementAngleSpeed = .2f;

    public float airSpeedModifier = 0.3f;

    public Animator _animator;

    public bool onFloor = false;

    private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.maxAngularVelocity = Speed * 2.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float rSpd = Speed;
        if (!onFloor) rSpd *= airSpeedModifier;

        // move the player based on camera position

        Vector3 forward = cameraPivot.forward;
        forward.y = 0.0f;

        Vector3 force = (forward * Input.GetAxis("Vertical") * rSpd) + (cameraPivot.right * Input.GetAxis("Horizontal") * rSpd);
        RaycastHit hit;
        if(!Physics.Raycast(transform.position + transform.up,force.normalized,out hit,.5f))
            m_rigidbody.AddForce(force);

        // TODO: have this based on the direction of the force
        MeshTransform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle((MeshTransform.localEulerAngles.z), -Input.GetAxis("Horizontal") * movementAngle, movementAngleSpeed));

        //if(MeshTransform.localEulerAngles)

        if (m_rigidbody.velocity.magnitude > MaxVelocity)
        {
            m_rigidbody.velocity = m_rigidbody.velocity.normalized * MaxVelocity;
        }

        if (Input.GetKey(KeyCode.Space) && onFloor)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, JumpSpeed, m_rigidbody.velocity.z);
        }

        // rotate player to face direction
        Vector3 lookPoint = m_rigidbody.velocity.normalized + transform.position;

        //if (onFloor) 
        lookPoint.y = transform.position.y;

        if (m_rigidbody.velocity.magnitude > 1.5f)
            transform.LookAt(lookPoint);

        print(m_rigidbody.velocity.magnitude);

        _animator.SetFloat("speed", m_rigidbody.velocity.magnitude);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "floor" || collision.gameObject.GetComponent<TagsExtended>().HasTag(TagsExtended.Tags.FLOOR))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, .1f))
                onFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor" || collision.gameObject.GetComponent<TagsExtended>().HasTag(TagsExtended.Tags.FLOOR))
        {
            onFloor = false;
        }
    }
}
