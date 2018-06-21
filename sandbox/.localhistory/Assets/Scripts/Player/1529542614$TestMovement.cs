using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

    private CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		controller.SimpleMove(Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")))
	}
}
