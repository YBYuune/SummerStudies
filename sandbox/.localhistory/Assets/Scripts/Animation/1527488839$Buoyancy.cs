﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour {

    public Vector3 buoyancy = Vector3.zero;
    public float buoyancyLimit = 0.0f;
    public float maxVel = 0.0f;

    private Rigidbody m_rigidbody;
    private Vector3 startPos;

    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        startPos = transform.position;

    }
	
	void Update () {

        if (m_rigidbody.velocity.magnitude <= maxVel || maxVel == 0.0f)
        {
            if(Vector3.Distance())
            m_rigidbody.AddForce(buoyancy);
        }

    }
}
