using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Transform cameraPivot;

    public Transform MeshTransform;

    public float MaxVelocity;
    public float Speed;
    public float JumpSpeed;
    public float MaxJumpHeight = 3.0f;
    public Vector3 ExtraGravity = new Vector3(0,0,0);

    public float movementAngle = 16.0f;
    public float movementAngleSpeed = .2f;

    public Animator _animator;

    public bool onFloor = false;
    private bool isJumping = false;
    private float YPosAtJump = -99;

    private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.maxAngularVelocity = Speed * 2.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // apply extra gravity
        m_rigidbody.AddForce(ExtraGravity);

        Move();

        if (m_rigidbody.velocity.magnitude > MaxVelocity)
        {
            Vector3 vel = ((m_rigidbody.velocity * new Vector3(1,0,1)).normalized * MaxVelocity);
            m_rigidbody.velocity = new Vector3(vel.x, m_rigidbody.velocity.y, vel.z);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (onFloor)
                isJumping = true;
            if (isJumping)
                Jump();
        }
        else
        {
            isJumping = false;
        }

        // rotate player to face direction
        Vector3 lookPoint = m_rigidbody.velocity.normalized + transform.position;
        lookPoint.y = transform.position.y;

        if (m_rigidbody.velocity.magnitude > 1.5f)
            transform.LookAt(lookPoint);

        // tell the animator we're moving
        _animator.SetFloat("speed", m_rigidbody.velocity.magnitude);
    }

    private void Move()
    {

        // move the player based on camera position

        Vector3 forward = cameraPivot.forward;
        forward.y = 0.0f;

        Vector3 force = (forward * Input.GetAxis("Vertical") * Speed) + (cameraPivot.right * Input.GetAxis("Horizontal") * Speed);
        RaycastHit hit;
        if (!Physics.Raycast(transform.position + transform.up, force.normalized, out hit, .5f))
            m_rigidbody.AddForce(force);

        // TODO: have this based on the direction of the force
        MeshTransform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle((MeshTransform.localEulerAngles.z), -Input.GetAxis("Horizontal") * movementAngle, movementAngleSpeed));
    }

    private void Jump()
    {
        if (YPosAtJump == -99)
            YPosAtJump = transform.position.y;

        Vector3 vel = m_rigidbody.velocity;
        vel.y = JumpSpeed;
        m_rigidbody.velocity = vel;

        if (transform.position.y - YPosAtJump >= MaxJumpHeight)
            isJumping = false;

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "floor" || collision.gameObject.GetComponent<TagsExtended>().HasTag(TagsExtended.Tags.FLOOR))
        {
            YPosAtJump = -99;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, .1f)) // check if the floor is below you, so you can't jump on walls
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
