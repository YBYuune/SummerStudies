using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float Speed;
    public float JumpSpeed;

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
        m_rigidbody.AddForce(Vector3.forward * Input.GetAxis("Vertical") * Speed);
        m_rigidbody.AddForce(Vector3.right * Input.GetAxis("Horizontal") * Speed);

        if (Input.GetKey(KeyCode.Space))
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, JumpSpeed, m_rigidbody.velocity.z);
        }
        print(m_rigidbody.velocity);
    }

    private void OnCollisionEnter(Collision collision)
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
