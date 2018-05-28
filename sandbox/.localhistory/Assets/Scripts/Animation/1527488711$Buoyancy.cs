using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour {

    public Vector3 buoyancy = Vector3.zero;
    public Vector3 buoyancyLimit = Vector3.zero;
    public float maxVel = 0.0f;

    private Rigidbody m_rigidbody;

    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {

        if (m_rigidbody.velocity.magnitude <= maxVel || maxVel == 0.0f)
        {
            m_rigidbody.AddForce(buoyancy);
        }

    }
}
