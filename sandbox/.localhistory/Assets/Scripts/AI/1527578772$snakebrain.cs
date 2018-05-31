using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SnakeBrain : MonoBehaviour {

    public Transform[] pieces;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float torqueDir = 1.0f;

        for (int i = 0; i < pieces.Length; i++)
        {

            pieces[i].GetComponent<Rigidbody>().AddRelativeTorque(Vector3.back * 15.0f / Mathf.Pow(i+1,2));
        }
	}
}
