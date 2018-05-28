using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour {

    public float buoyancy = 10.0f;
    public float buoyancyLimit = 0.0f;
    public float maxVel = 0.0f;

    private Rigidbody m_rigidbody;
    private Vector3 startPos;

    void Start () {
        startPos = transform.position;

    }
	
	void Update () {

        m_rigidbody = GetComponent<Rigidbody>();
        if (m_rigidbody.velocity.magnitude <= maxVel || maxVel == 0.0f)
        {
            if(Vector3.Distance(transform.position,startPos) < buoyancyLimit)
                m_rigidbody.AddForce(buoyancy);
        }

    }
}
