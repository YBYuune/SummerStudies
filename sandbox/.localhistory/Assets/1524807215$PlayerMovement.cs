using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float Speed;
    public float JumpSpeed;

    private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_rigidbody.AddForce(Vector3.forward * Input.GetAxis("Vertical") * Speed);
        m_rigidbody.AddForce(Vector3.right * Input.GetAxis("Horizontal") * Speed);

        if(Input.GetKeyDown(KeyCode.Space))

    }
}
