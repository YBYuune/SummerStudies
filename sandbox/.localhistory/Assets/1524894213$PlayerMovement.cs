using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float maxSpeed;

    public float Speed;
    public float JumpSpeed;

    public float airSpeedModifier = 0.3f;

    private bool onFloor = false;

    private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.maxAngularVelocity = Speed * 2.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float rSpd = Speed;
        if (!onFloor) rSpd *= airSpeedModifier;

        m_rigidbody.AddForce(Vector3.forward * Input.GetAxis("Vertical") * rSpd);
        m_rigidbody.AddForce(Vector3.right * Input.GetAxis("Horizontal") * rSpd);

        if (m_rigidbody.velocity.magnitude > 5.0)
        {
            m_rigidbody.velocity.Normalize();
            m_rigidbody.velocity = m_rigidbody.velocity * 
        }

        if (Input.GetKey(KeyCode.Space) && onFloor)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, JumpSpeed, m_rigidbody.velocity.z);
        }

        print(m_rigidbody.velocity.magnitude);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            onFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            onFloor = false;
        }
    }
}
