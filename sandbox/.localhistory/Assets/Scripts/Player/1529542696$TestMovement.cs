﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TestMovement : MonoBehaviour {

    private CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        controller.SimpleMove(new Vector3(-Input.GetAxis("Vertical"),0, Input.GetAxis("Horizontal")));
	}
}
